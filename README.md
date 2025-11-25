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


# Variations to the above specification:
1. To exit the program, **CTRL+C** must be used - this is a requirement in the `IHostedService` implementation.
1. Ticket purchasing by any player is limited to the funds they have available, the ticket cost and the maximum number of tickets that may be purchased in one transaction. This means it is possible for a player to still have funds, but not have enough to buy a ticket, therefore if this happens, the minimum ticket purchasing requirement is relaxed.
1. The game itself runs only once, but can easily be made to run in a loop until all funds are exhausted for all players.
1. When starting, the lottery system generates a pool of potential players, from which a selection can be made for the game itself.
1. For each the game, the system _always_ selects the human player, followed by a random selection of players from the pool to make up the total number of players required for the game.
1. Output to the console is using the Microsoft.Extensions.Logging package, with DI configuration to log to the console. This results in a lot of extra lines in the output showing the log-level involved. Use of a custom `ILogger` implementation would easily sort this out and direct output to the appropriate location(s) in the required formats.
1. When performing some "automated" tasks, such as pulling together the players and buying tickets, a number of `LogDebug` statements are emitted, showing the underlying operational decisions. <p>e.g. `Player 1 chooses to buy 6 tickets`</p>
1. During the payout stage of the lottery process, each payment to a player is detailed in a `LogDebug` statement, similar to the following: <p>`Player 5 wins on 'Grand Prize' with 1 ticket, receiving $39.50`</p>
1. In calculating payouts, house profits etc., there are a number of options that could be explored, such as limiting the payout to be a result of a `Math.Floor` operation, meaning all results will be dragged down to the nearest integral amount. Other possibilities are to limit the payments to the nearest 0.10 amount, again with options to use `Round` or `Floor` values.
1. Again, in calculating the payouts, where there is a fractional part to the proportion of tickets that could win, the prizes are allocated to the integral number of tickets. <p>e.g. 79 tickets are sold in total, therefore the "second tier" prizes would be allocated to 7.9 winning tickets, which is not possible. Therefore the winning ticket count is determined to be 7. The total share of prize funds for the tier would be 23.70, which would equate to each ticket winning 3.38, with the remaining 0.04 being allocated to the house profits.</p>
1. Alternate wallet formatters are possible, with the output then using the appropriate currency symbol for the specified `CultureInfo` object being used, such as showing a &pound;, &euro;, or &yen; symbol. The formatter can also take note of the number of decimal places the local currency uses, so for those currencies that have 3 places (such as the Bahraini and Jordanian Dinar), or those with no places, such as the Japanese Yen, output would be formatted correctly. For the "default" option, this would mean the symbol is $ and uses 2 decimal places, so results look similar to this: `$0.00`.
1. Allowing different `CultureInfo` objects to be used could permit a currency conversion between the prize fund currency and local currency amounts, with the output in an appropriate format, be that as prize fund or local currency, or both!