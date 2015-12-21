#sudo-ku: a journey towards more and better javascript

This repository tells the tale of a journey. In particular, of a journey of a classical .Net developer trying to get more and more involved into modern javascript applications.

I mainly report the tale to myself -- the code experiments on my local drive are becoming too numerous to be managable; hence the idea of using a public forum to single out a documented trajectory that goes from javascript naivity into hopefully somewhat more javascript wisdom. Eventual interest of others in this path is purely incidental.

The thread in the contributions to this repo is a game of sudoku: a small application that presents users with a sudoku challenge and invites them to complete the puzzle while validating their moves.

The starting point of this journey is a setup which you will be familiar with if you have a background like me in one of the main statically typed, server oriented languages like .net. In stage-1, there's a working example of an implementation using ASP.NET MVC5 with a single javascript script file relying on nothing but JQuery.

What I plan to do is systematically move the 'core' and it's code into scripts that execute in the browser. In the end, I want to have a SPA version of the game that runs in a browser as well as as a mobile and UWP app.

I'm sure this journey will confront me with numerous challenges:
- using git properly, to begin with
- learning javascript properly
- learning the DOM and how browsers provide a runtime for javascript
- mastering client side templating
- supporting disconnected scenario's
- testing
- code organisation: mimic classes, support modules, apply coding patterns
- learning ES6 or typescript features
- learning the nodejs development tools for building javascript applications outside of Visual Studio
- addressing cross-browser compatibility issues

None of these challenges will be met by prematurely  referencing and using a library or framework such as Angular or Aurelia. I very well might do so on my next project; but I hope this journey will have provided me by then with enough context to use them wisely.
