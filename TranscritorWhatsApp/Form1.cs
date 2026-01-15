using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whisper.net;
using Whisper.net.Ggml;

namespace TranscritorWhatsApp
{
    public partial class Form1 : Form
    {
        private string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");

        // Separei as memórias: uma para onde busca áudios, outra para onde salva textos
        private string _ultimaPastaAudios = string.Empty;
        private string _ultimaPastaDestino = string.Empty;

        public Form1()
        {
            InitializeComponent();
            ConfigurarInterface();

            // Ligações dos botões
            this.btnAdicionar.Click += new EventHandler(this.btnAdicionar_Click);
            this.btnRemover.Click += new EventHandler(this.btnRemover_Click); // Novo botão
            this.btnIniciar.Click += new EventHandler(this.btnIniciar_Click);
            this.btnSelecionarDestino.Click += new EventHandler(this.btnSelecionarDestino_Click);
            this.rbMesmaPasta.CheckedChanged += (s, e) => AlternarModoSalvamento();
            this.rbOutraPasta.CheckedChanged += (s, e) => AlternarModoSalvamento();
        }

        private void ConfigurarInterface()
        {
            cmbModelo.DataSource = Enum.GetValues(typeof(GgmlType));
            cmbModelo.SelectedItem = GgmlType.Small;

            int threadsDisponiveis = Environment.ProcessorCount;
            // Limita a 8 para evitar travamentos do Whisper em CPUs com muitos núcleos
            int threadsSeguras = 8;
            int calculoThreads = threadsDisponiveis > 2 ? threadsDisponiveis - 2 : 1;
            int threadsSugeridas = Math.Min(calculoThreads, threadsSeguras);

            numThreads.Minimum = 1;
            numThreads.Maximum = threadsDisponiveis;
            numThreads.Value = threadsSugeridas;

            rbMesmaPasta.Checked = true;
            btnSelecionarDestino.Enabled = false;
            txtCaminhoDestino.Enabled = false;
            txtCaminhoDestino.ReadOnly = true;
        }

        private void AlternarModoSalvamento()
        {
            bool usarOutraPasta = rbOutraPasta.Checked;
            btnSelecionarDestino.Enabled = usarOutraPasta;
            txtCaminhoDestino.Enabled = usarOutraPasta;
            if (!usarOutraPasta) txtCaminhoDestino.Clear();
        }

        // --- BOTÃO REMOVER (NOVO) ---
        private void btnRemover_Click(object sender, EventArgs e)
        {
            // Remove de trás para frente para não bagunçar os índices
            if (lstArquivos.SelectedIndices.Count > 0)
            {
                for (int i = lstArquivos.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    lstArquivos.Items.RemoveAt(lstArquivos.SelectedIndices[i]);
                }
            }
            else
            {
                MessageBox.Show("Selecione arquivos na lista para remover.");
            }
        }

        // --- BOTÃO SELECIONAR PASTA DE DESTINO ---
        private void btnSelecionarDestino_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                // Se já escolheu uma pasta antes, abre nela
                if (!string.IsNullOrEmpty(_ultimaPastaDestino) && Directory.Exists(_ultimaPastaDestino))
                {
                    fbd.InitialDirectory = _ultimaPastaDestino;
                }

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtCaminhoDestino.Text = fbd.SelectedPath;
                    _ultimaPastaDestino = fbd.SelectedPath; // Memoriza para a próxima
                }
            }
        }

        // --- BOTÃO ADICIONAR ÁUDIOS ---
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Áudios|*.opus;*.ogg;*.wav;*.mp3|Todos|*.*";
                ofd.RestoreDirectory = true; // Impede que o Windows mude o diretório global do app

                // Usa a memória da pasta de ÁUDIOS (separada da pasta de destino)
                if (!string.IsNullOrEmpty(_ultimaPastaAudios) && Directory.Exists(_ultimaPastaAudios))
                {
                    ofd.InitialDirectory = _ultimaPastaAudios;
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileNames.Length > 0)
                    {
                        _ultimaPastaAudios = Path.GetDirectoryName(ofd.FileNames[0]);
                    }

                    foreach (var file in ofd.FileNames)
                    {
                        if (!lstArquivos.Items.Contains(file)) lstArquivos.Items.Add(file);
                    }
                }
            }
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            if (lstArquivos.Items.Count == 0) { MessageBox.Show("Lista vazia."); return; }
            if (rbOutraPasta.Checked && string.IsNullOrWhiteSpace(txtCaminhoDestino.Text)) { MessageBox.Show("Selecione destino."); return; }

            if (!File.Exists(ffmpegPath))
            {
                MessageBox.Show($"O arquivo ffmpeg.exe não foi encontrado.\nVerifique se ele está na mesma pasta do executável.", "Erro FFmpeg", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetInterfaceState(false);

            var modeloSelecionado = (GgmlType)cmbModelo.SelectedItem;
            string modelFileName = $"ggml-{modeloSelecionado.ToString().ToLower()}.bin";

            try
            {
                if (!File.Exists(modelFileName))
                {
                    lblStatus.Text = $"Baixando modelo {modeloSelecionado}...";
                    Application.DoEvents();
                    await BaixarModelo(modeloSelecionado, modelFileName);
                }

                int total = lstArquivos.Items.Count;
                progressBar.Maximum = total;
                progressBar.Value = 0;

                using var whisperFactory = WhisperFactory.FromPath(modelFileName);
                using var processor = whisperFactory.CreateBuilder()
                    .WithLanguage("pt")
                    .WithThreads((int)numThreads.Value)
                    .Build();

                for (int i = 0; i < total; i++)
                {
                    string arquivoInput = lstArquivos.Items[i].ToString();
                    lblStatus.Text = $"({i + 1}/{total}): {Path.GetFileName(arquivoInput)}";

                    string pastaFinal = rbMesmaPasta.Checked ? Path.GetDirectoryName(arquivoInput) : txtCaminhoDestino.Text;
                    string caminhoTxtFinal = Path.Combine(pastaFinal, Path.GetFileNameWithoutExtension(arquivoInput) + ".txt");

                    await TranscreverArquivo(processor, arquivoInput, caminhoTxtFinal);
                    progressBar.Value = i + 1;
                }

                lblStatus.Text = "Concluído!";
                MessageBox.Show("Transcrição Finalizada!");

                if (chkLimpar.Checked) { lstArquivos.Items.Clear(); progressBar.Value = 0; lblStatus.Text = "Pronto."; }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro Geral: {ex.Message}");
            }
            finally
            {
                SetInterfaceState(true);
            }
        }

        private async Task BaixarModelo(GgmlType type, string fileName)
        {
            using var httpClient = new HttpClient();
            var downloader = new WhisperGgmlDownloader(httpClient);
            using var modelStream = await downloader.GetGgmlModelAsync(type, QuantizationType.NoQuantization, CancellationToken.None);
            using var fileWriter = File.Create(fileName);
            await modelStream.CopyToAsync(fileWriter);
        }

        private async Task TranscreverArquivo(WhisperProcessor processor, string inputPath, string outputPath)
        {
            string tempWav = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".wav");

            try
            {
                await ConverterComFFmpeg(inputPath, tempWav);

                // FileShare.ReadWrite ajuda a evitar travamentos de arquivo em uso
                using var fileStream = new FileStream(tempWav, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var outputWriter = new StreamWriter(outputPath);

                await foreach (var result in processor.ProcessAsync(fileStream))
                {
                    if (!string.IsNullOrWhiteSpace(result.Text))
                    {
                        await outputWriter.WriteLineAsync(result.Text.Trim());
                    }
                }
            }
            finally
            {
                if (File.Exists(tempWav)) try { File.Delete(tempWav); } catch { }
            }
        }

        private Task ConverterComFFmpeg(string input, string output)
        {
            var tcs = new TaskCompletionSource<bool>();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-y -i \"{input}\" -ar 16000 -ac 1 -c:a pcm_s16le \"{output}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.EnableRaisingEvents = true;
            process.Exited += (s, e) =>
            {
                if (process.ExitCode == 0) tcs.SetResult(true);
                else tcs.SetException(new Exception($"FFmpeg falhou (Código {process.ExitCode})."));
                process.Dispose();
            };

            try { process.Start(); }
            catch (Exception ex) { throw new Exception($"Erro ao iniciar FFmpeg: {ex.Message}"); }

            return tcs.Task;
        }

        private void SetInterfaceState(bool enabled)
        {
            btnAdicionar.Enabled = enabled;
            btnRemover.Enabled = enabled; // Trava o botão remover durante a execução
            btnIniciar.Enabled = enabled;
            cmbModelo.Enabled = enabled;
            chkLimpar.Enabled = enabled;
            numThreads.Enabled = enabled;
            grpSaida.Enabled = enabled;
            if (enabled) AlternarModoSalvamento();
        }
    }
}