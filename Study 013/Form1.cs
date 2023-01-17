namespace Study_013
{
    public partial class Form1 : Form
    {
        public const String API_KEY = "MsbYRk9CUhFwYzQPJ5h4y439";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void nukki(String path)
        {
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Headers.Add("X-Api-Key", API_KEY);
                formData.Add(new ByteArrayContent(File.ReadAllBytes(path)), "image_file", "file.jpg");
                formData.Add(new StringContent("auto"), "size");
                var response = client.PostAsync("https://api.remove.bg/v1.0/removebg", formData).Result;

                if (response.IsSuccessStatusCode)
                {
                    FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                    response.Content.CopyToAsync(fileStream).ContinueWith((copyTask) => { fileStream.Close(); });
                }
                else
                {
                    Console.WriteLine("Error: " + response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nukki(openFileDialog1.FileName);
            }
        }
    }
}