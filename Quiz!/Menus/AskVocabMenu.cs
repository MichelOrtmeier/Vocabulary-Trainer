﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vokabeltrainer.Menus.SelectOption;
using Vokabeltrainer.Vocabs;
using Vokabeltrainer.Management;
using System.Diagnostics;

namespace Vokabeltrainer.Menus
{
    internal class AskVocabMenu : Menu
    {
        public override void DisplayMenu()
        {
            if (SubjectManager.GetSubjectNames().Count() != 0)
            {
                new SelectOptionMenu(SelectOptionTemplates.SubjectMenu);
                new SelectOptionMenu(SelectOptionTemplates.AskingDirectionMenu);
                Console.Clear();
                RequestLoop();
                SummarizeRequest();
                SubjectManager.CurrentSubject.Save();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Es sind noch keine Vokabeln vorhanden!");
                Console.WriteLine("Bitte gebe zunächst Vokabeln ein!");
                Console.ReadKey();
                Console.ResetColor();
            }
            new SelectOptionMenu(SelectOptionTemplates.StartMenu);
        }

        public void RequestLoop()
        {
            Queue<VocabRequest> requests = RequestManager.CurrentRequest.Requests;
            while(requests.Count > 0)
            {
                VocabRequest vocab = requests.Dequeue();
                Console.WriteLine($"Abfrage -- {SubjectManager.CurrentSubject.Name} -- Noch {requests.Count + 1} Vokabeln");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                Console.Write("Wort: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(vocab.GetQuestion());
                string input = InputAnswer();
                Console.ResetColor();
                bool isRightInput = vocab.IsRightInput(input);
                vocab.LogInput(isRightInput);
                SubjectManager.CurrentSubject.ChangeVocab(vocab);
                if (isRightInput)
                {
                    Console.Write("Richtig: ");
                }
                else
                {
                    Console.Write("Falsch: ");
                    requests.Enqueue(vocab);
                }
                vocab.PrintReaction(input);
                Console.WriteLine();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private string InputAnswer()
        {
            string answer;
            while (true)
            {
                Console.ResetColor();
                Console.Write("Antwort: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                answer = Console.ReadLine();
                if (answer.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FEHLER: Ungültige Eingabe!");
                }
                else
                    break;
            }
            return answer;
        }

        private void SummarizeRequest()
        {
            Console.WriteLine($"Super gemacht: {(int)RequestManager.CurrentRequest.PercentRight()}% richtig");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            Console.WriteLine("Merke");
            Console.WriteLine("-----");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Vocab vocab in RequestManager.CurrentRequest.WrongVocabs)
            {
                Console.WriteLine(vocab.ToString());
            }
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
