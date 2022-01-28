string head = "";
string chest = "";
string belly = "";
string legs = "";

string lang = "en";
string welcomeMessage = "";
string inputLetterMessage = "";
string alreadyTriedCharacterMessage = "";
string endGameMessage = "";
string triedCharactersDisplayMessage = "";

int actualWordIndex = 0;
List<Char> triedCharacters = new List<Char>();
List<Char> correctCharacters = new List<Char>();
String[] listOfWords = { "primeiro", "segundo", "terceiro" };
bool newLetter = false;
bool endOfGame = false;
bool nextWord = false;

Console.Clear();
    
Console.Write("Escolha sua língua / Choose your language: ");
lang = Console.ReadLine() ?? "en";

switch (lang) {
    case "pt":
        welcomeMessage = "Bem vindo ao jogo da Forca!\n" + "Aperte qualquer tecla pra começar...";
        inputLetterMessage = "Entre com uma letra: ";
        alreadyTriedCharacterMessage = "Você já tentou essa letra, tente outra!";
        endGameMessage = "Fim do jogo.\n" + "Muito obrigado por jogar!";
        triedCharactersDisplayMessage = "Caracteres Tentados: ";
        break;
    case "en":
        welcomeMessage = "Welcome to Hangman Game!\n" + "Press any key to begin...";
        inputLetterMessage = "Enter a character: ";
        alreadyTriedCharacterMessage = "You already tried this character, try another one!";
        endGameMessage = "End of game.\n" + "Thank you for playing!";
        triedCharactersDisplayMessage = "Tried Characters: ";
        break;
}

Console.Clear();
Console.WriteLine(welcomeMessage);

Console.ReadLine();

do
{
    Console.Clear();
    newLetter = false;
    nextWord = false;

    Console.WriteLine(DrawHangman(head, chest, belly, legs));
    writeWordGame(listOfWords[actualWordIndex], triedCharacters, correctCharacters, triedCharactersDisplayMessage);

    do
    {
        Console.Write(inputLetterMessage);
        Char.TryParse(Console.ReadLine(), out char test);

        if (test == '#')
        {
            endOfGame = true;
            newLetter = true;
        }
        else
        {
            if (listOfWords[actualWordIndex].Contains(test) && !correctCharacters.Contains(test))
            {
                correctCharacters.Add(test);
                newLetter = true;
            }
            else if (!triedCharacters.Contains(test) && !correctCharacters.Contains(test))
            {
                triedCharacters.Add(test);

                switch (triedCharacters.Count)
                {
                    case 1:
                        head = "(_)";
                        break;
                    case 2:
                        chest = "/|\\";
                        break;
                    case 3:
                        belly = "|";
                        break;
                    case 4:
                        legs = "/ \\";
                        break;
                    default:
                        break;
                }
                newLetter = true;
            }
            else
            {
                Console.WriteLine(alreadyTriedCharacterMessage);
            }
        }
    } while (!newLetter);

    if (isTheWordComplete(correctCharacters, listOfWords[actualWordIndex]))
    {
        nextWord = true;
        actualWordIndex++;
        triedCharacters.Clear();
        head = "";
        chest = "";
        legs = "";
        belly = "";
    }

    if (actualWordIndex == listOfWords.Length)
    {
        endOfGame = true;
    }

    if (triedCharacters.Count == 4)
    {
        endOfGame = true;
    }

    if (nextWord)
    {
        triedCharacters.Clear();
        correctCharacters.Clear();
    }

    // if (!endOfGame) * poderia ter alguma coisa aqui pra terminar o jogo, tipo uma animação.

} while (!endOfGame);


Console.Clear();
Console.WriteLine(endGameMessage);

// functions 

/*
 *
 */
static string DrawHangman(string head, string chest, string belly, string legs)
{
    string hangman = "     ------- \n" +
                "    |/      |\n" +
                "    |       |\n" +
               $"    |      {head}\n" +
               $"    |      {chest}\n" +
               $"    |       {belly}\n" +
               $"    |      {legs}\n" +
                "    |\n" +
                "____|____";

    return hangman;
}

/*   Summary: 
 *      Write the game's layout on the screen, as well the 
 *      'Tried Chars' and 'Correct Chars' variables.
 *   Return: 
 *      nothing (void).
 */
static void writeWordGame(String word, List<Char> listTriedChars, List<Char> listCorrectChars, string triedCharMessage)
{
    Console.WriteLine("\n--------------------------------------");

    Console.Write("\n\t");

    for (int i = 0; i < word.Length; i++)
    {
        if (word[i] == ' ')
        {
            Console.Write("  ");
        }
        else if (listCorrectChars.Contains(word[i]))
        {
            int j = listCorrectChars.FindIndex(0, listCorrectChars.Count(), x => x == word[i]);
            Console.Write($"{listCorrectChars[j]} ");
        }
        else
        {
            Console.Write("_ ");
        }

    }

    Console.Write("\n\n" + triedCharMessage);

    for (int i = 0; i < listTriedChars.Count; i++)
    {
        if (i != listTriedChars.Count - 1)
        {
            Console.Write($"'{listTriedChars[i]}', ");
        }
        else
        {
            Console.Write($"'{listTriedChars[i]}'.");
        }
    }

    Console.WriteLine("\n--------------------------------------");
}

/*   Summary:
 *      Compare the actual word with the list of correct chars.
 *   Return:
 *       a bool type. If the list of correct chars has all the
 *       the letters of the word, return true. Else, return false. 
 */
static bool isTheWordComplete(List<Char> correctCharList, String word)
{
    bool isComplete = true;

    for (int i = 0; i < word.Length; i++)
    {
        if (!correctCharList.Contains(word[i]))
        {
            isComplete = false;
        }
    }

    return isComplete;
}