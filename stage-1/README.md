# Stage-1

This is the starting point: a puzzle game that works, involves miimal javascript but leaves a lot to be desired for.

In this initial stage, sudo-ku is architected and implemented in a way I feel relatively comfortable with: 
anything complex like the data structures and algorithms for generating and playing sudoku's are designed in .NET to live on a web server. 
The actual UI consists of a dynamically genereted puzzle page and a little bit of javascript that provides feedback on 
user input while avoiding page refreshes.

I guess this setup would be quite typical for a few years ago: all heavy lifting (in our case: the construction of relatively complex data 
structures and the modelling of relatively complex algorithms) is done on the server side, where a framework such as .NET can be run.  

