## API Reference
http://localhost:{PORT}}/api/...
# GET:
/sports
	-- Get all sports
/sports/{sports_id}/games
	-- [WIP]get all available games of specific sport
/sports/{sports_id}/games/{location_id}/{radius}
	-- [WIP] get all available games for specific sport within an area
/sports/{sports_id}/players
	-- [WIP] get all players for specific game
/sports/{sports_id}/games/referees
	-- [WIP] get all referee for specific game
/games/{game_id}
	-- get specific game.
/users/{sports_id}/players/
	-- search all players of specific sport
/users/players/{user_id}/
	-- get specific player
/users/{sports_id}/referees/
	-- [WIP] get all referees for specific sport

#PUT:
/sports/{sports_id}/games/players/{user_id}				-Add/Remove player to/from game
{
  "playerId": 35,		-- Same as {user_id}
  "gameId": 102,		--
  "name": {
    "nameId": 28,
    "firstName": "Queen",
    "middleName": "raw",
    "lastName": "Kong"
  },
  "birthDate": "1989-01-23T07:34:31",
  "email": "Gabriel.Russell@telus.net",
  "game": null
}

/sports/{sports_id}/games/
