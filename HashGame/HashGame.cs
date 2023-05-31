
namespace HashGame
{
    class HashGame
    {
        private bool gameOver;
        private char[] positions;
        private char turn;
        private int filledQuantity;
        public HashGame()
        {
            gameOver = false;
            positions = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            turn = 'X';
            filledQuantity = 0;
        }

        public void Start()
        {
            while (!gameOver)
            {
                RenderTable();
                ReadUserChoice();
                RenderTable();
                CheckGameOver();
                ChangeTurn();
            }
        }

        private void ChangeTurn()
        {
            turn = turn == 'X' ? 'O' : 'X';
        }

        private void CheckGameOver()
        {
            if (filledQuantity > 5)
                return;

            if (DiagonalVictory() || VerticalVictory() || HorizontalVictory())
            {
                gameOver = true;
                Console.WriteLine($"Game Over! {turn} Victory!");
                return;
            }

            if (filledQuantity is 9)
            {
                gameOver = true;
                Console.WriteLine("Draw!");
            }
        }

        private bool HorizontalVictory()
        {
            bool line1victory = positions[0] == positions[1] && positions[0] == positions[2];
            bool line2victory = positions[3] == positions[4] && positions[3] == positions[5];
            bool line3victory = positions[6] == positions[7] && positions[6] == positions[8];

            return line1victory || line2victory || line3victory;
        }
        private bool VerticalVictory()
        {
            bool line1victory = positions[0] == positions[3] && positions[0] == positions[6];
            bool line2victory = positions[1] == positions[4] && positions[1] == positions[7];
            bool line3victory = positions[2] == positions[5] && positions[2] == positions[8];

            return line1victory || line2victory || line3victory;
        }
        private bool DiagonalVictory()
        {
            bool line1victory = positions[2] == positions[4] && positions[2] == positions[6];
            bool line2victory = positions[0] == positions[4] && positions[0] == positions[8];

            return line1victory || line2victory; 
        }


        private void ReadUserChoice()
        {
            Console.WriteLine($"{turn} Turn!");

            bool conversion = int.TryParse(Console.ReadLine(), out int chosenPosition);

            while (!conversion || !ValidateUserChoice(chosenPosition))
            {
                Console.WriteLine("The chosen field is invalid, please enter a number between 1 and 9 that is available in the table.");
                conversion = int.TryParse(Console.ReadLine(), out chosenPosition);
            }

            FillChoice(chosenPosition);
        }

        private void FillChoice(int chosenPosition)
        {
            int index = chosenPosition - 1;

            positions[index] = turn;
            filledQuantity++;
        }

        private bool ValidateUserChoice(int chosenPosition)
        {
            var index = chosenPosition - 1;

            return positions[index] != '0' && chosenPosition != 'X';
        }

        private void RenderTable()
        {
            Console.Clear();
            Console.WriteLine(GetTable());
        }

        private string GetTable()
        {
            return $"__{positions[0]}__|__{positions[1]}__|__{positions[2]}__\n" +
                    $"__{positions[3]}__|__{positions[4]}__|__{positions[5]}__\n" +
                    $"__{positions[6]}  | {positions[7]}  |  {positions[8]}   \n\n";
        }
    }
}
