# cutechessfix
tool to restart a CUTECHESS-CLI tournament after engine's crash

Prerequisites :
copy cutechessfix.exe to your CUTECHESS-CLI folder<p>

command : cutechessfix.exe your_cutechess-cli_arguments<p>

# when to use cutechessfix ?
CUTECHESS-CLI already has a "-recover" option which restarts an engine after its crash but it doesn't always work.<br>
Especially with engines with a learning feature that often crash on their first run.<br>
If the user is present or has a remote command utility, he can manually restart CUTECHESS-CLI.<br>
Depending on the type of tournaments (eg gauntlet) or with learning farms, it quickly becomes tedious !<br>
This is where CUTECHESSFIX comes in.<p>

# most common scenario
1°) We try to run a gauntlet like this but engines crash :<p>

<i>cutechess-cli.exe -tournament gauntlet -engine conf="Engine" -engine conf="Opponent1" -engine conf="Opponent2" -engine conf="Opponent3" -engine conf="Opponent4" -each tc=60+1 -games 500 fi -repeat -recover</i><p>

* If "Engine" or "Opponent1" crashes, we have to run it again.<br>
* When "Engine" and "Opponent1" have played 500 games, if "Engine" or "Opponent2" crashes, we have to edit the command line in order to remove <i>"-engine conf="Opponent1"</i> and run it again.<br>
* It will be the same problem if "Engine" or "Opponent3" crashes when "Engine" and "Opponent2" have played 500 games...<p>
<br>
2°) The first thing to do is to split this gauntlet into individual tourneys :<p>

<i>cutechess-cli.exe -engine conf="Engine" -engine conf="Opponent1" -each tc=60+1 -games 500 fi -repeat -recover<br>
cutechess-cli.exe -engine conf="Engine" -engine conf="Opponent2" -each tc=60+1 -games 500 fi -repeat -recover<br>
cutechess-cli.exe -engine conf="Engine" -engine conf="Opponent3" -each tc=60+1 -games 500 fi -repeat -recover<br>
cutechess-cli.exe -engine conf="Engine" -engine conf="Opponent4" -each tc=60+1 -games 500 fi -repeat -recover</i><p>

* So no need to edit the main command line to delete one or more opponents after an engine's crash.<p>
<br>
3°) In each command line, if we replace "cutechess-cli.exe" by "cutechessfix.exe", the individual tournament concerned will restart automatically after an engine's crash :<p>

<i>cutechessfix.exe -engine conf="Engine" -engine conf="Opponent1" -each tc=60+1 -games 500 fi -repeat -recover<br>
cutechessfix.exe -engine conf="Engine" -engine conf="Opponent2" -each tc=60+1 -games 500 fi -repeat -recover<br>
cutechessfix.exe -engine conf="Engine" -engine conf="Opponent3" -each tc=60+1 -games 500 fi -repeat -recover<br>
cutechessfix.exe -engine conf="Engine" -engine conf="Opponent4" -each tc=60+1 -games 500 fi -repeat -recover</i><p>
