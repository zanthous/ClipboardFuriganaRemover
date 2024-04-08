using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClipboardFuriganaRemover2
{
	struct Word
	{
		public List<string> kanji;
		public List<string> kana;
		public List<string> definitions;

		public Word(List<string> kanji_in, List<string> kana_in, List<string> definitions_in)
		{
			kanji = kanji_in;
			kana = kana_in;
			definitions = definitions_in;
		}
	}

	static class Program
	{
		//static MemoryStream stream;
		//static Dictionary<string, Word> wordDictionary = new Dictionary<string, Word>();
		//static List<string> tokens = new List<string> { "keb", "reb", "gloss", "entry" };
		//List<Word> words = new List<Word>();


		[STAThread]static Task Main(string[] args)
		{
			ApplicationConfiguration.Initialize();
			//var sr = new StreamReader("JMdict_e.xml");
			//var xmltext = sr.ReadToEnd();
			//byte[] byteArray = Encoding.UTF8.GetBytes(xmltext);
			//stream = new MemoryStream(byteArray);

			//LoadDictionary(stream);

			//pray the load is done

			string previousClipboardText = string.Empty;
			while(true)
			{
				if(Clipboard.ContainsText())
				{
					string currentText = Clipboard.GetText();
					if(currentText != previousClipboardText)
					{
						string filteredText = FilterText(currentText);
						if(filteredText != currentText)
						{
							Clipboard.SetText(filteredText);
							//if(wordDictionary.ContainsKey(filteredText))
							{
								//StringBuilder sb = new StringBuilder();
								//var theWord = wordDictionary[filteredText];
								//if(theWord.definitions != null)
								//{
								//	for(int i = 0; i < theWord.definitions.Count; i++)
								//	{
								//		sb.AppendLine(theWord.definitions[i]);
								//	}
								//}
								//if(theWord.kana != null && theWord.kana.Count > 0)
								//{
								//	sb.AppendLine(theWord.kana[0]);
								//}
								//Clipboard.SetText(sb.ToString());
							}

						}
						previousClipboardText = Clipboard.GetText();
					}
				}
				Thread.Sleep(10); // Wait for 10ms before checking the clipboard again to reduce resource usage.
			}
		}

		static string FilterText(string text)
		{
			string pattern = "[\u3040-\u309F]+";
			string output = Regex.Replace(text, pattern, "");
			// Example filter: Remove a specific word. Customize this method according to your needs.
			return output;
		}

		//static void LoadDictionary(System.IO.Stream stream)
		//{
		//	StringBuilder resultCsv = new StringBuilder();

		//	XmlReaderSettings settings = new XmlReaderSettings();
		//	settings.DtdProcessing = DtdProcessing.Parse;
		//	settings.Async = true;

		//	//Create c# dictionary of the xml dictionary
		//	using(XmlReader reader = XmlReader.Create(stream, settings))
		//	{
		//		string currentToken = "";

		//		while(!reader.EOF)
		//		{
		//			Word word = new Word(new List<string>(), new List<string>(), new List<string>());

		//			ReadToEntry(reader);
		//			reader.ReadAsync();

		//			while(!reader.EOF)
		//			{
		//				currentToken = ReadToValue(reader);

		//				//next entryreached , all data for current entry already read
		//				if(currentToken == "entry")
		//					break;

		//				switch(currentToken)
		//				{
		//					case "keb":
		//						word.kanji.AddRange(GetValues(reader, currentToken));
		//						break;
		//					case "reb":
		//						word.kana.AddRange(GetValues(reader, currentToken));
		//						break;
		//					case "gloss":
		//						word.definitions.AddRange(GetValues(reader, currentToken));
		//						for(int i = 0; i < word.definitions.Count; i++)
		//						{
		//							word.definitions[i] = word.definitions[i].Replace(",", "A");
		//						}
		//						break;
		//				}
		//			}

		//			foreach(var spelling in word.kanji)
		//			{
		//				if(!wordDictionary.ContainsKey(spelling))
		//				{
		//					wordDictionary.Add(spelling, word);
		//				}
		//			}
		//			foreach(var spelling in word.kana)
		//			{
		//				if(!wordDictionary.ContainsKey(spelling))
		//				{
		//					wordDictionary.Add(spelling, word);
		//				}
		//			}
		//		}
		//	}
		//}

		//static void ReadToEntry(XmlReader reader)
		//{
		//	while(reader.Name != "entry")
		//	{
		//		reader.Read();
		//	}
		//}

		//static string ReadToValue(XmlReader reader)
		//{
		//	while(!tokens.Contains(reader.Name) && !reader.EOF)
		//	{
		//		reader.Read();
		//	}
		//	return reader.Name;
		//}

		//static List<string> GetValues(XmlReader reader, string token)
		//{
		//	var definitions = new List<string>();

		//	while(reader.Name == token && !reader.EOF)
		//	{
		//		reader.Read();
		//		if(reader.NodeType == XmlNodeType.Text)
		//		{
		//			definitions.Add(reader.Value);
		//		}
		//		reader.Read();
		//	}
		//	return definitions;
		//}

	}
}