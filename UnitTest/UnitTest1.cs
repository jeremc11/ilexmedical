using Microsoft.VisualStudio.TestTools.UnitTesting;
using IlexMedical;
using System.Collections.Generic;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidSudoku()
        {
            List<string> examples = new List<string>
            {
                "008317000004205109000040070327160904901450000045700800030001060872604000416070080",
                "040890630000136820800740519000467052450020700267010000520003400010280970004050063",
                "048301560360008090910670003020000935509010200670020010004002107090100008150834029"
            };

            foreach (var row in examples)
            {
                Console.Write("Validating sudoku: {0}", row);
                Sudoku sudoku = new Sudoku(row);
                Assert.AreEqual(true, sudoku.ValidateSudoku());
            }
        }

        [TestMethod]
        public void InvalidRow()
        {
            // duplicate value 8 in the first row
            string row = "008387000004205109000040070327160904901450000045700800030001060872604000416070080";
            Sudoku sudoku = new Sudoku(row);
            Assert.AreEqual(false, sudoku.ValidateSudoku());
        }

        [TestMethod]
        public void InvalidColumn()
        {
            // duplicate value 3 in the first column
            string row = "308317000004205109000040070327160904901450000045700800030001060872604000416070080";
            Sudoku sudoku = new Sudoku(row);
            Assert.AreEqual(false, sudoku.ValidateSudoku());
        }

        [TestMethod]
        public void InvalidSquare()
        {
            // duplicate value 3 in the second square
            string row = "008337000004205109000040070327160904901450000045700800030001060872604000416070080";
            Sudoku sudoku = new Sudoku(row);
            Assert.AreEqual(false, sudoku.ValidateSudoku());
        }

        [TestMethod]
        public void InvalidCharsCount()
        {
            string row = "564";
            Assert.ThrowsException<Exception>(() =>
            {
                Sudoku sudoku = new Sudoku(row);
            });
        }
    }
}
