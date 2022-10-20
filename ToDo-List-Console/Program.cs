using System;
using System.IO;
using System.Collections.Generic;

namespace ToDo_List_Console
{
	class Program
	{
		static void Main(string[] args)
		{
			StreamReader listReader = new StreamReader("ToDoList.txt");
			List<string> wholeTextInList = new List<string>();

			while (listReader.Peek() >= 0)
			{
				string readedLn = listReader.ReadLine();
				wholeTextInList.Add(readedLn);
			}

			listReader.Close();

			string splitingTextLn = "=======================================";
			string wholeText = "";

			foreach (string ln in wholeTextInList)
			{
				wholeText += ln + "\r\n";
			}

			if (wholeText == "")
			{
				Console.WriteLine(splitingTextLn);
				Console.WriteLine("List is empty.");
			}
			else
			{
				Console.WriteLine("Text from list:");
				Console.WriteLine(splitingTextLn);
				Console.Write(wholeText);
			}

			Console.WriteLine(splitingTextLn + "\n");

			bool nextWhile = false;

			do 
			{ 
				try
				{
					if (wholeText != "")
					{
						Console.Write("Removes some lines of list? (y/n): ");
						char lnRm = Console.ReadKey().KeyChar;

						if (lnRm == 'y')
						{
							Console.Write("\nWrite all numbers of lines which you want remove split by spaces: ");
							string nsOfLnRmString = Console.ReadLine().Trim() + ' ';
							List<uint> listOfNsOfRmLn = new List<uint>();

							foreach (char charInNsOfRmLn in nsOfLnRmString)
							{
								if (charInNsOfRmLn == ' ')
								{
									listOfNsOfRmLn.Add(Convert.ToUInt32(nsOfLnRmString.Split(' ')[0]));

									if (nsOfLnRmString.Split(' ').Length > 1)
									{
										nsOfLnRmString = nsOfLnRmString.Substring(nsOfLnRmString.Split(' ')[0].Length + 1);
									}
									else
									{
										break;
									}
								}
							}

							string[] wholeTextInArray = wholeTextInList.ToArray();
							List<string> newWholeTextInList = new List<string>();

							for (uint i = 1; i <= wholeTextInArray.Length; i++)
							{
								if(!listOfNsOfRmLn.Contains(i))
								{
									newWholeTextInList.Add(wholeTextInArray[i - 1]);
								}
							}

							File.Delete("ToDoList.txt");
							StreamWriter listRewriter = File.CreateText("ToDoList.txt");

							foreach (string ln in newWholeTextInList)
							{
								listRewriter.WriteLine(ln);
							}

							listRewriter.Close();

							if (newWholeTextInList.Count < 1)
							{
								Console.WriteLine("\n" + splitingTextLn);
								Console.WriteLine("New list is empty.");
							}
							else
							{
								Console.WriteLine("\nText from new list:");
								Console.WriteLine(splitingTextLn);

								foreach (string ln in newWholeTextInList)
								{
									Console.WriteLine(ln);
								}
							}

							Console.WriteLine(splitingTextLn);
						}
						else if (lnRm != 'n')
						{
							throw new Exception("Wrong input.");
						}
					}

					Console.Write("\nAdd some lines to list? (y/n): ");
					char lnMk = Console.ReadKey().KeyChar;

					if (lnMk == 'y')
					{
						Console.WriteLine();

						List<string> newLines = new List<string>();
						bool mkNextLn = false;

						do
						{
							mkNextLn = false;

							Console.Write("\nWrite new line: ");
							newLines.Add(Console.ReadLine());

							Console.Write("Write antoher new line? (y/n): ");
							char newLnMk = Console.ReadKey().KeyChar;

							if (newLnMk == 'y')
							{
								mkNextLn = true;
							}
							else if (newLnMk != 'n')
							{
								throw new Exception("Wrong input.");
							}

						} while (mkNextLn);

						StreamWriter listWriter = new StreamWriter("ToDoList.txt");

						foreach (string newLn in newLines)
						{
							listWriter.WriteLine(newLn);
						}

						listWriter.Close();

						StreamReader listReader2 = new StreamReader("ToDoList.txt");
						string wholeText2 = listReader2.ReadToEnd();

						if (wholeText2 == "")
						{
							Console.WriteLine("\n" + splitingTextLn);
							Console.WriteLine("List is empty.");
						}
						else
						{
							Console.WriteLine("\n\nText from list:");
							Console.WriteLine(splitingTextLn);
							Console.Write(wholeText2);
						}

						Console.WriteLine(splitingTextLn);
					}
					else if (lnMk != 'n')
					{
						throw new Exception("Wrong Input.");
					}
				}
				catch (Exception e)
				{
					
					Console.Write("\nSomething is went wrong ({0}). Try editing list again (y/n): ", e.Message);

					if(Console.ReadKey().KeyChar == 'y')
					{
						Console.Clear();
						nextWhile = true;
					}
					else
					{
						Environment.Exit(0);
					}
				}
			} while (nextWhile);
		}
	}
}