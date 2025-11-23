# Simplified Lottery Game

## The Problem
Build a simplified lottery game using C#. The solution should include a console application as the user interface.

Please feel free to add any improvements you can identify along the way. Any enhancements are appreciated, but they are not essential.

## Lottery Game Mechanics
A lottery is a gambling competition in which people obtain numbered tickets, each of which has the chance of winning one prize. At a set time, winning tickets are randomly drawn from a pool holding all purchased tickets and winners are notified.

## Ticket Purchase
The user (Player 1) is prompted via the console to purchase their desired number of tickets. The remaining participants are CPU players, sequentially numbered as Player 2, Player 3, and so on. The number of tickets per CPU player is randomly picked

##  Player Limits and Costs
* The total number of players in each lottery game should be between 10 and 15.
* All players (human and CPU) are limited to purchasing between 1 and 10 tickets.
* Each player begins with a starting balance of $10.
* Tickets are priced at $1 each.

## Prize Determination
The program should determine the prize allocation by using the following rules:
| Prize Level | Description |
| :---: | --- |
| **Grand Prize** | A single ticket must be awarded a prize equivalent to 50% of the total ticket revenue. |
| **Second Tier** | 10% of the tickets must share 30% of the total ticket revenue equally. |
| **Third Tier** |  20% of the tickets must share 10% of the total ticket revenue equally. |

The program should determine the winners and prize allocation by following these rules:
1. One ticket can win, at most, one prize allocation - i.e. one ticket _cannot_ win multiple prizes
1. Where a prize allocation for a tier results in a fractional amount, the closest equal split is taken and the remainder is allocated to the house funds.
1. Any remaining revenue from ticket sales is allocated to the house funds.

## Result Presentation
The program should output a list of the winning players, including the count of their winning tickets (in brackets next to the player identifier, e.g., {playerId} (winning tickets count)), the amount they have won, and the house profit, all printed to the console.

### Example:
```console
Welcome to the Simplified Lottery, Player 1!

* Your digital balance: $10.00
* Ticket Price: $1.00 each

How many tickets do you want to buy Player 1?
5

13 other CPU Players have also purchased tickets.

Ticket Draw Results:

* Grand Prize: Player 7(1) wins $50.00!
* Second Tier: Players 1(2), 3(1), 5(1), 6(1), 9(3), 11(1), 13(1) win $3.00 per ticket!
* Third Tier : Players 1(1), 2(1), 3(1), 4(2), 5(2), 6(1), 7(2), 8(1), 9(3), 10(1), 11(1), 12(2), 13(1), 14(1) win $0.50 per winning ticket!

Congratulations to the winners!

House Profit: $10.00
```