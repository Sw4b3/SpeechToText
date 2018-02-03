using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace SpeechToText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        Choices commands = new Choices();
        GrammarBuilder grammarBuilder = new GrammarBuilder();
        

        public MainWindow()
        {
            commands.Add(new string[] { "hello", "print my name", "how are you", "stop listening","close" });
            grammarBuilder.Append(commands);
            Grammar grammar = new Grammar(grammarBuilder);
            engine.LoadGrammarAsync(grammar);
            engine.SetInputToDefaultAudioDevice();
            engine.SpeechRecognized += engine_speechRecognized;
            speechSynthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);

            InitializeComponent();
        }

        private void engine_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text) {
                case "hello":
                    textbox1.Text = "Hello Andrew";
                    speechSynthesizer.Speak("Hello Andrew");
                    break;
                case "print my name":
                    textbox1.Text="Andrew Schwabe";
                    speechSynthesizer.Speak("Andrew Schwabe");
                    break;
                case "how are you":
                    textbox1.Text = "I am good";
                    speechSynthesizer.Speak("I am good");
                    break;
                case "stop listening":
                    engine.RecognizeAsyncStop();
                    textbox1.Text = "Mic stopped";
                    speechSynthesizer.Speak("Mic stopped");
                    break;
                case "close":
                    this.Close();
                    break;
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            engine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            engine.RecognizeAsyncStop();
        }
    }
}
