class MasterMind {

    static char[]? code;

    private static void Main() {
        var result = false;
        var chancesLeft = 12;
        code = GenerateCode();

        while(!result && chancesLeft > 0) {
          Console.Write("Please enter 4-digit (1-6) guess: ");
          var input = Console.ReadLine();
    
          if (input == null || input.Length != 4 || !input.All(x => char.IsNumber(x) && x >= '1' && x <= '6')) {
            Console.WriteLine("Please enter a valid guess of 4 digits between 1 and 6.");
            continue;
          }

          result = CalcPrintResults(input);
          chancesLeft--;
        }

        Console.WriteLine(result ? "You solved it!" : "You lose :(");
    }

    private static bool CalcPrintResults(string guess) {
        var chars = guess.ToCharArray();

        // calc pluses by finding digit overlap
        var pluses = guess.Zip(code, (x, y) => x == y).Count(x => x);

        // calc all matches (plus and minus) by finding intersects and matches
        var matches = 0;
        var intersects = guess.Intersect(code);
        foreach(var intersect in intersects) {
          var codeMatches = code.Count(x => x == intersect);
          var guessMatches = guess.Count(x => x == intersect);

          //account for digit limitations in either code or guess
          matches += Math.Min(codeMatches, guessMatches);
        }
        var minuses = matches - pluses;

        Console.WriteLine(string.Empty.PadLeft(pluses, '+') + string.Empty.PadLeft(minuses, '-'));
        return pluses == 4;
    }

    private static char[] GenerateCode() {
      var rand = new Random();
      var codeString = rand.Next(1,6).ToString() + rand.Next(1,6).ToString() + rand.Next(1,6).ToString() + rand.Next(1,6).ToString();
      return codeString.ToCharArray();
    }
}