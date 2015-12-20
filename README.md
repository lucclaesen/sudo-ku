#sudo-ku: a journey towards more and better javascript

This repository tells the tale of a journey. In particular, of a journey of a classical .Net developer trying to get more and more involved into modern javascript applications.

I mainly report the tale to myself -- the code experiments on my local drive are becoming too numerous to be managable; hence the idea of using a public forum to single out a documented trajectory that goes from javascript naivity into hopefully somewhat more javascript wisdom. Eventual interest of others in this path is purely incidental.

The thread in the contributions to this repo is a game of sudoku. The little application that will return in every sub folder of this repository generates a sudoku puzzles and validates input the user in solving it.

How the sudo-ku application creates and validates puzzles will be constant throughout the different stages of this coding journey:
- the most fundamental entity in this solution is a solver that represents a sudoku puzzle as a exact cover problem. It translates the problem space in terms of a doubly linked lists and runs Donald Knuth's beautifull Dancing Link algorithm (Algorithm-X). This is an efficient version of a brute force, constraint solving algorithm.
- I implemented a random generator for solvable puzzles just like they did it for NintendoDS -- in fact, an approach that generates puzzles with a unique solution.

I won't say more about Sudoku's than this: it's quite immaterial to the intent of this repo.

The true value of the sudo-ku journey lies in the path between the way I would have made the application, say, two years ago and how (I hope) would do it in say two months time.

In stage-1, I present a working puzzle game with all the puzzle generation and validation logic in an asp.net mvc application. When the play-page is requested, the web server populates the DOM with the challenge (the *givens* of the puzzle). In the browser, there's a script that captures user input to the puzzle and delegates it's validation to the web server by means of an ajax call.

