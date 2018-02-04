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
using System.IO;

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


        public MainWindow()
        {

            string[] lines = File.ReadAllLines(".\\grammar.txt");
            commands.Add(lines);
            Grammar grammar = new Grammar(new GrammarBuilder(commands));
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
                    textbox1.AppendText( "Hello \n");
                    speechSynthesizer.Speak("Hello");
                    break;
                case "how are you":
                    textbox1.AppendText("I am good \n");
                    speechSynthesizer.Speak("I am good");
                    break;
                case "stop listening":
                    engine.RecognizeAsyncStop();
                    textbox1.AppendText("Mic stopped \n");
                    speechSynthesizer.Speak("Mic stopped");
                    break;
                case "play audio":
                    textbox1.AppendText("Playing audio \n");
                    speechSynthesizer.Speak("Playing audio");
                    break;
                case "stop audio":
                    textbox1.AppendText("Stopping Audio \n");
                    speechSynthesizer.Speak("Stopping Audio");
                    break;
                case "close":
                    this.Close();
                    break;
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textbox1.AppendText("Starting Mic \n");
            speechSynthesizer.Speak("Starting Mic");
            engine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox1.AppendText("Stopping Mic \n");
            speechSynthesizer.Speak("Stopping Mic");
            engine.RecognizeAsyncStop();
        }
    }
}
