

namespace EjemploJSONWinForm
{
    using System;
    using System.Windows.Forms;
    using System.IO;
    using Newtonsoft.Json;
    using System.Dynamic;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using System.Linq;
    /// <summary>
    /// https://es.ourcodeworld.com/articulos/leer/54/como-manipular-y-usar-json-con-c-en-winforms
    /// </summary>
    public partial class Form1 : Form
    {
        string Valor1, Valor2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dynamic myObject = new ExpandoObject();

            myObject.nombre = "Our Code World";
            myObject.sitioweb = "http://ourcodeworld.com";
            myObject.lenguaje = "es-CO";
            
            List<string> articulos = new List<string>();
            articulos.Add("Como usar JSON con C#");
            articulos.Add("Top 5: Mejores calendarios jQuery");
            articulos.Add("Otro articulo ... ");

            myObject.articulos = articulos;

            string json = JsonConvert.SerializeObject(myObject);

            //  Console.WriteLine(json);
            textBox1.Text = json;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string googleSearchText = @"{
                'responseData': {
                'results': [
                 {
                 'GsearchResultClass': 'GwebSearch',
                 'unescapedUrl': 'http://en.wikipedia.org/wiki/Paris_Hilton',
                 'url': 'http://en.wikipedia.org/wiki/Paris_Hilton',
                 'visibleUrl': 'en.wikipedia.org',
                 'cacheUrl': 'http://www.google.com/search?q=cache:TwrPfhd22hYJ:en.wikipedia.org',
                 'title': '<b>Paris Hilton</b> - Wikipedia, the free encyclopedia',
                 'titleNoFormatting': 'Paris Hilton - Wikipedia, the free encyclopedia',
                 'content': '[1] In 2006, she released her debut album...'
                 },
                 {
                 'GsearchResultClass': 'GwebSearch',
                 'unescapedUrl': 'http://www.imdb.com/name/nm0385296/',
                 'url': 'http://www.imdb.com/name/nm0385296/',
                 'visibleUrl': 'www.imdb.com',
                 'cacheUrl': 'http://www.google.com/search?q=cache:1i34KkqnsooJ:www.imdb.com',
                 'title': '<b>Paris Hilton</b>',
                 'titleNoFormatting': 'Paris Hilton',
                 'content': 'Self: Zoolander. Socialite <b>Paris Hilton</b>...'
                 }
                ],
                'cursor': {
                 'pages': [
                 {
                 'start': '0',
                 'label': 1
                 },
                 {
                 'start': '4',
                 'label': 2
                 },
                 {
                 'start': '8',
                 'label': 3
                 },
                 {
                 'start': '12',
                 'label': 4
                 }
                 ],
                 'estimatedResultCount': '59600000',
                 'currentPageIndex': 0,
                 'moreResultsUrl': 'http://www.google.com/search?oe=utf8&ie=utf8...'
                }
                },
                'responseDetails': null,
                'responseStatus': 200
                }";

            JObject googleSearch = JObject.Parse(googleSearchText);

            // Obtener la propiedades result en una lista 
            IList<JToken> results = googleSearch["responseData"]["results"].Children().ToList();

            // Serializa resultados JSON a un objeto .NET
            IList<SearchResult> searchResults = new List<SearchResult>();

            foreach (JToken result in results)
            {
                SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(result.ToString());
                searchResults.Add(searchResult);
            }

            // Lista las propiedades del resultado de la busqueda (IList)
            foreach (SearchResult item in searchResults)
            {
                // Console.WriteLine(item.Title);
                //  Console.WriteLine(item.Content);
                //Console.WriteLine(item.Url);
                listBox3.Items.Add(item.Title);
                listBox4.Items.Add(item.Content);
                listBox5.Items.Add(item.Url);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string json = @"{
                           'CPU': 'Intel',
                           'PSU': '500W',
                           'Drives': [
                             'DVD read/writer'
                             /*(broken)*/,
                             '500 gigabyte hard drive',
                             '200 gigabype hard drive'
                                   ]
                                }";

            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Valor1 = string.Format("Token: {0}, Valor: {1}", reader.TokenType, reader.Value);                  

                    listBox1.Items.Add(Valor1);
                }
                else
                {
                    Valor2 = string.Format("Token: {0}", reader.TokenType);
                   listBox2.Items.Add(Valor2);
                }
            }
        }
    }
}
