﻿using System;

namespace Vokabeltrainer.Vocabs
{
    [Serializable]
    internal class Vocab
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Form { get; set; }
        public byte Level { get; protected set; }

        public Vocab(string question, string answer, string form, byte level)
        {
            Question = question;
            Answer = answer;
            Form = form;
            Level = level;
        }

        public Vocab(Vocab vocab)
        {
            vocab.Question = Question;
            vocab.Answer = Answer;
            vocab.Form = Form;
            vocab.Level = Level;
        }

        public void ChangeToVocab(Vocab changedVocab)
        {
            Question = changedVocab.Question;
            Answer = changedVocab.Answer;
            Form = changedVocab.Form;
            Level = changedVocab.Level;
        }

        public string GetAnswer(AskingDirection direction)
        {
            if (direction == AskingDirection.QuestionToAnswer && Form.Length != 0)
                return Form + " " + Answer;
            else if (direction == AskingDirection.QuestionToAnswer && Form.Length == 0)
                return Answer;
            else if (Form.Length != 0)
                return Question + " " + Form;
            else
                return Question;
        }
    }
}
