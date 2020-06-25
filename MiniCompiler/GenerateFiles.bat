Gplex.exe /verbose /out:Scanner.cs kompilator.lex
Gppg.exe /gplex /verbose /out:Parser.cs kompilator.y
pause > nul
Gppg.exe kompilator.y
pause > nul