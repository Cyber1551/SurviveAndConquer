
	var WIDTH = 1200;
	var HEIGHT = 1000;



	var socket = io();
	//var $ = require("jquery");
	var signDiv = document.getElementById("signDiv");
	var regDiv = document.getElementById("regDiv");
	var lobbyDiv = document.getElementById("lobbyDiv");
	//var matchLabel = document.getElementById("matchLabel");
	var goldLabel = document.getElementById("gold");
	var gameDiv = document.getElementById("gameDiv");
	//var playersDiv = document.getElementById("playersDiv");
	var attackL = document.getElementById("attackL");
	var armorL = document.getElementById("armorL");
	var attackSpdL = document.getElementById("attackSpdL");
	var critL = document.getElementById("critL");
	var lifeStealL = document.getElementById("lifeStealL");
	var attackTT = document.getElementById("attackTT");
	var armorTT = document.getElementById("armorTT");
	var attackSpdTT = document.getElementById("attackSpdTT");
	var critTT = document.getElementById("critTT");
	var lifeStealTT = document.getElementById("lifeStealTT");
	var lifeRegenTT = document.getElementById("lifeRegenTT");
	var lethalityTT = document.getElementById("lethalityTT");
	var healthDiv = document.getElementById("healthDiv");
	var healthL = document.getElementById("healthL");
	var killL = document.getElementById("killL");
	var deathL = document.getElementById("deathL");



	//SignDiv
	var signDivUsername = document.getElementById("signDiv-username");
	var signDivPassword = document.getElementById("signDiv-password");
	var signDivSignIn = document.getElementById("signDiv-signIn");
	var signDivRegister = document.getElementById("signDiv-register");

	//RegDIv
	var regDivUsername = document.getElementById("regDiv-username");
	var regDivPassword = document.getElementById("regDiv-password");
	var regDivEmail = document.getElementById("regDiv-email");
	var regDivPasswordRepeat = document.getElementById("regDiv-passwordRepeat");
	var regDivSignIn = document.getElementById("regDiv-signIn");
	var regDivRegister = document.getElementById("regDiv-register");

	//LobbyDiv
	var userLabel = document.getElementById("userLabel");
	var lobbyDivfindMatch = document.getElementById("lobbyDiv-findMatch");
	var lobbyDivSignOut = document.getElementById("lobbyDiv-signOut");
	var lobbyModeSel = document.getElementById ("lobbyModeSel");
	var lobbyDivCancel = document.getElementById("lobbyDivCancel");

	var statsDivBack = document.getElementById("statsDivBack");
	var storeDiv = document.getElementById("store");
	var levelL = document.getElementById("levelL");


		var canvas = document.getElementById("ctx");
	var ctx = canvas.getContext("2d");

	var pointerLocked = false;
	canvas.requestPointerLock = canvas.requestPointerLock ||
                            canvas.mozRequestPointerLock;

	document.exitPointerLock = document.exitPointerLock ||
                           document.mozExitPointerLock;


	signDivRegister.onclick = function()
	{
		signDiv.style.display = 'none';
		regDiv.style.display = 'inline-block';
	}
	regDivSignIn.onclick = function()
	{
		regDiv.style.display = 'none';
		signDiv.style.display = 'inline-block';
	}
	signDivSignIn.onclick = function()
	{
		//console.log(signDivUsername.value);
		socket.emit('signIn', {username:signDivUsername.value, password:signDivPassword.value});
	}
	regDivRegister.onclick = function()
	{

		socket.emit('register', {username:regDivUsername.value, password:regDivPassword.value, rPassword:regDivPasswordRepeat.value, email:regDivEmail.value});
	}
	lobbyDivfindMatch.onclick = function()
	{
		$("#loader").css("display", "inline-block");
		var matchType = lobbyModeSel.options [lobbyModeSel.selectedIndex].value;
		socket.emit('matchMake', {matchType:matchType});
	}
	lobbyDivSignOut.onclick = function()
	{
		$("#lobbyDivCancel").css("display", "none");
		$("#lobbyDiv-findMatch").css("display", "inline-block");
		//$("#lobbyDiv").css("display", "none");
		//$("#signDiv").css("display", "inline-block");
		location.reload();
		//Refesh Page ---- Coming Soon

	}
	lobbyDivCancel.onclick = function()
	{
		//var matchType = lobbyModeSel.options[lobbyModeSel.selectedIndex].value;
		socket.emit("cancelSearch");
	}
	lobbyDivStats.onclick = function()
	{
		//var matchType = lobbyModeSel.options[lobbyModeSel.selectedIndex].value;
		$("#lobbyDiv").css("display", "none");
		$("#playerStatsDiv").css("display", "inline-block");
	}
	statsDivBack.onclick = function()
	{
		$("#lobbyDiv").css("display", "inline-block");
		$("#playerStatsDiv").css("display", "none");
	}
	socket.on('backToLobby', function(data)
	{
		loadStore();
		$("#elementL").text("Not Set");
		$("#gameDiv").css("display", "none");
		$("#lobbyDiv").css("display", "inline-block");
		$("#loader").css("display", "none");
		$("#lobbyWins").text(data.wins);
		$("#lobbyLosses").text(data.losses);
		var totalGames = data.wins + data.losses;
		var winR = ((data.wins / totalGames) * 100).toFixed(2) + "%";
		$("#lobbyWinR").text(winR);
		$("body").css("background-color", "rgb(51, 153, 255)");
		$("#lobbyDivCancel").css("display", "none");
		$("#lobbyDiv-findMatch").css("display", "inline-block");
		console.log('The pointer lock status is now unlocked');
		pointerLocked = false;
		
		document.exitPointerLock();
		//socket.emit('disconnect');
	});

	socket.on('signInResponse', function(data)
	{
		if (data.success)
		{
			signDiv.style.display = 'none';
			lobbyDiv.style.display = 'inline-block';
			$("#lobbyWins").text(data.wins);
			$("#lobbyLosses").text(data.losses);
			var totalGames = data.wins + data.losses;
			var winR = ((data.wins / totalGames) * 100).toFixed(2) + "%";
			$("#lobbyWinR").text(winR);
			$("#userLabel").text(data.username);
		}
		else{
			$("#signInfo").css("display", "inline-block");
			$("#signInfo").text("Sign In Unsuccessful");

		}
	});

	socket.on('registerResponse', function(data)
	{
		if (data.success)
		{
			$("#signDiv").css("display", "inline-block");
			$("#regDiv").css("display", "none");
			$("#signInfo").css("display", "inline-block");
			$("#signInfo").text("You can now Login!");
		}
		else{
			$("#regInfo").css("display", "inline-block");
			$("#regInfo").text("Register Unsuccessful");

		}
	});

	socket.on("inGame", function()
	{

		lobbyDiv.style.display = 'none';
		$('body').css("background-color", "white");
		gameDiv.style.display = 'inline-block';
	});
	socket.on("updateLobbyScore", function(data)
	{
		$("#lobbyWins").text(data.wins);
		$("#lobbyLosses").text(data.losses);
		var totalGames = data.wins + data.losses;
		var winR = ((data.wins / totalGames) * 100).toFixed(2) + "%";
		$("#lobbyWinR").text(winR);
	});

	socket.on("cancelButton", function(data)
	{
		if (data.value == true)
		{
			$("#lobbyDivCancel").css("display", "inline-block");
			$("#lobbyDiv-findMatch").css("display", "none");
		}
		else
		{
			$("#lobbyDivCancel").css("display", "none");
			$("#lobbyDiv-findMatch").css("display", "inline-block");
			$("#loader").css("display", "none");
		}

	});
	socket.on("removeSelfId", function()
	{
		selfId = null;
	})
	function surrender()
	{
		if (!selfId)
			return;
		socket.emit('sendPMToServer', {
				user: "team",
				message: "Surrender Started!"
			});
		socket.emit("surrender", {playerId: selfId});
	}

	var chatText = document.getElementById("chat-text");
	var chatInput = document.getElementById("chat-input");
	var chatForm = document.getElementById("chat-form");


	ctx.font = "30px Arial";

	canvas.onclick = function() {
		canvas.requestPointerLock();
	}

	document.addEventListener('pointerlockchange', lockChangeAlert, false);
	document.addEventListener('mozpointerlockchange', lockChangeAlert, false);

	function lockChangeAlert() {
		if (document.pointerLockElement === canvas ||
			document.mozPointerLockElement === canvas && pointerLocked == false) {

			console.log('The pointer lock status is now locked');
			pointerLocked = true;
			document.addEventListener("mousemove", updatePosition, false);
		} else {
			console.log('The pointer lock status is now unlocked');
			pointerLocked = false;
			document.removeEventListener("mousemove", updatePosition, false);
		}
	}
	var entryCoor = {x: -1, y:-1};
	function updatePosition(e)
	{
		if (entryCoor.x == -1 || entryCoor.y == -1)
		{
			entryCoor = getPosition(canvas, e);
		}

		var moveX = e.movementX || e.mozMovementX || e.webkitMovementX || 0;
		var moveY = e.movementY || e.mozMovementY || e.webkitMovementY || 0;
		moveX = Math.clamp(moveX, -50, 50);
		moveY = Math.clamp(moveY, -50, 50);

		entryCoor.x = entryCoor.x + moveX;
		entryCoor.y = entryCoor.y + moveY;


		if (entryCoor.x > WIDTH - 5)
		{
			entryCoor.x = WIDTH - 5;
		}
		else if (entryCoor.x < 0)
		{
			entryCoor.x = 0;
		}
		if (entryCoor.y > HEIGHT - 5)
		{
			entryCoor.y = HEIGHT - 5;
		}
		else if(entryCoor.y < 0)
		{
			entryCoor.y = 0;
		}
		//console.log(moveX + ":" + moveY);

	}

	(function(){Math.clamp=function(a,b,c){return Math.max(b,Math.min(c,a));}})();

	 function getPosition(canvas, event) {
        var x = new Number();
        var y = new Number();

        if (event.x != undefined && event.y != undefined) {
            x = event.x;
            y = event.y;
        }
        else // Firefox method to get the position
        {
            x = event.clientX + document.body.scrollLeft +
                    document.documentElement.scrollLeft;
            y = event.clientY + document.body.scrollTop +
                    document.documentElement.scrollTop;
        }

        x -= canvas.offsetLeft;
        y -= canvas.offsetTop;

        return {x:x, y:y};
	}


	var Img = {};
	//Neutral
	Img.player = new Image();
	Img.player.src = '/client/img/Player/player.png'; 
	//Fire
	Img.playerFire = new Image();
	Img.playerFire.src = '/client/img/Player/playerFire.png'; 
	//Water
	Img.playerWater = new Image();
	Img.playerWater.src = '/client/img/Player/playerWater.png';
	//Earth
	Img.playerEarth = new Image();
	Img.playerEarth.src = '/client/img/Player/playerEarth.png';
	//Wind
	Img.playerWind = new Image();
	Img.playerWind.src = '/client/img/Player/playerWind.png';
	//Lightning
	Img.playerLightning = new Image();
	Img.playerLightning.src = '/client/img/Player/playerLightning.png';
	
	//Neutral
	Img.playerShield = new Image();
	Img.playerShield.src = '/client/img/PlayerShield/playerShield.png'; 
	//Fire
	Img.playerFireShield = new Image();
	Img.playerFireShield.src = '/client/img/PlayerShield/playerFireShield.png'; 
	//Water
	Img.playerWaterShield = new Image();
	Img.playerWaterShield.src = '/client/img/PlayerShield/playerWaterShield.png';
	//Earth
	Img.playerEarthShield = new Image();
	Img.playerEarthShield.src = '/client/img/PlayerShield/playerEarthShield.png';
	//Wind
	Img.playerWindShield = new Image();
	Img.playerWindShield.src = '/client/img/PlayerShield/playerWindShield.png';
	//Lightning
	Img.playerLightningShield = new Image();
	Img.playerLightningShield.src = '/client/img/PlayerShield/playerLightningShield.png';
	
	
	Img.map1 = new Image();
	Img.map1.src = '/client/img/Maps/testMap1.png';
	Img.map2 = new Image();
	Img.map2.src = '/client/img/Maps/testMap2.png';
	Img.map3 = new Image();
	Img.map3.src = '/client/img/Maps/testMap3.png';


	function getPoint(c1, c2, radius, angle)
	{
		return [c1 + Math.cos(angle) * radius, c2 + Math.sin(angle) * radius];
	}

	//Init
	var Player = function(initPack)
	{
		var self = {};
		self.id = initPack.id;
		self.user = initPack.user;
		self.x = initPack.x;
		self.y = initPack.y;
		self.updatedX = 0;
		self.updatedY = 0;
		self.hpMax = initPack.hpMax;
		self.hp = self.hpMax;
		self.kills = initPack.kills;
		self.deaths = initPack.deaths;
		self.gold = initPack.gold;
		self.mouseAngle = initPack.mouseAngle;
		self.latency = 0;
		self.isShielding = initPack.isShielding;
		self.shieldMax = 100;
		self.shield = initPack.shield;
		self.stats = initPack.stats;
		self.map = initPack.map;
		self.roomId = initPack.roomId;
		self.level = 1;
		self.expMax = initPack.expMax;
		self.exp = 0;
		self.team = initPack.team;
		self.isGoal = false;
		self.canMove = true;
		self.overclocked = false;
		self.elementType = initPack.elementType;
		self.movementSpd = initPack.maxSpd;
		self.sprite = initPack.sprite;
		self.spriteShield = initPack.spriteShield;
		self.draw = function()
		{
			//chatText.innerHTML += self.name + "<br />";
			if (Player.list[selfId].roomId !== self.roomId)
				return;
			var px = self.x - Player.list[selfId].x + WIDTH/2;
			var py = self.y - Player.list[selfId].y + HEIGHT/2;
			//console.log("x: " + self.x + "y: " + self.y + "xx: " + px + "; yy:  " + py)

			socket.emit("updateXY", {playerId:self.id, xx:px, yy:py});

			ctx.strokeStyle = 'black';
			ctx.strokeRect(px-50, py-60, 100, 10);
			var hpWidth = 100 * (self.hp / self.hpMax);
			ctx.fillStyle = 'red';
			ctx.fillRect(px - 50, py - 60, hpWidth, 10);
			var shieldWidth = 100 * self.shield / self.shieldMax;
			ctx.strokeRect(px-50, py-50, 100, 5);
			ctx.fillStyle = 'blue';
			ctx.fillRect(px - 50, py - 50, shieldWidth, 5);

			
			ctx.font = "20px Arial";
				
			ctx.fillStyle = self.team;
			ctx.fillText(self.user, px - 20, py - 65);
				
			

			var width = Img.player.width;
			var height = Img.player.height;
			//self.x = self.x - 10;


			//ctx.stroke();

			var rad = (self.mouseAngle + 90) * Math.PI / 180;
			/*if (self.isShielding && self.shield > 0)
			{
				var shieldRad = Math.PI + rad;
				var midX = self.x + 32 * Math.cos(shieldRad + Math.PI / 2);
				var midY = self.y + 32 * Math.sin(shieldRad + Math.PI / 2);
				var mid = [midX, midY];
				var right = getPoint(self.x, self.y, 32, shieldRad + Math.PI);
				var leftHalf = getPoint(self.x, self.y, 32, shieldRad + Math.PI / 4);
				var rightHalf = getPoint(self.x, self.y, 32, shieldRad + 3 * Math.PI / 4 )
				var left = getPoint(self.x, self.y, 32, shieldRad);


				socket.emit("shieldValues", {mid: mid, left: left, right: right, rHalf: rightHalf, lHalf: leftHalf});

				ctx.fillRect(mid[0], mid[1], 5, 5);
				ctx.fillRect(right[0], right[1], 5, 5);
				ctx.fillRect(rightHalf[0], rightHalf[1], 5, 5);
				ctx.fillRect(leftHalf[0], leftHalf[1], 5, 5);
				ctx.fillRect(left[0], left[1], 5, 5);
				ctx.beginPath();
				ctx.arc(self.x, self.y, 32, 0 + shieldRad, Math.PI + shieldRad);
				ctx.stroke();
			}*/
			var spriteImage = new Image();
			spriteImage.src = self.sprite;
			
			var spriteShieldImage = new Image();
			spriteShieldImage.src = self.spriteShield;
			ctx.translate(px + (width / 2) - 30, py + (height / 2) - 30 );

			ctx.rotate(rad);
			if (self.isShielding && self.shield > 0)
			{
				ctx.drawImage(spriteShieldImage, (width / 2 * (-1)), height / 2 * (-1), width, height);
			} 
			else
			{
				ctx.drawImage(spriteImage, (width / 2 * (-1)), height / 2 * (-1), width, height);

			}
			//chatText.innerHTML+="" + self.mouseAngle + "<br />";


			ctx.rotate(rad * (-1));
			ctx.translate((px + width / 2) * (-1) + 30, (py + height / 2) * (-1) + 30);



			//ctx.font = "10px Arial";
			//ctx.fillText(self.kills, self.x, self.y-60);
			//ctx.font = "30px Arial";
		}
		
		Player.list[self.id] = self;

		return self;
	}
	Player.list = {};

	/*$("#respawnL").text("10 second strategy time!");
	setTimeout(function()
	{
		$("#respawnL").text("Capture the point!");
		socket.emit("setCanMove", {value:true});
	}, 10000);
*/
	
	/*var Damage = function()
	{
		var self = {};
		self.amount = 0;
		self.text = "";
		self.target = null;
		self.type = null;
		self.x = null;
		self.y = null;
		self.time = 500; //Milliseconds
		self.setAllValues = function(data)
		{
			self.target = data.playerHit;
			self.amount = data.amount;
			self.type = data.type;
			self.x = self.target.updatedX;
			self.y = self.target.updatedY;
		}
		self.draw = function()
		{

		//var playerHit = self.target;
		var col = null;
		var fnt = null;
		switch (self.type)
			{
				case "damage":
					self.text = "-" + self.amount;
					fnt = "15px Arial";
					col = "red";
				break;
				case "crit":
					self.text = "#" + self.amount;
					fnt = "20px Arial";
					col = "#b30000";
				break;
				case "heal":
					self.text = "+" + self.amount;
					fnt = "15px Arial";
					col = "green";
				break;
			}

			var yoffset = 80 + genRandomNumber(-10, 10);
			var xoffset = genRandomNumber(-20, 20);
			//var amount = data.amount;

			var move = 0;
			while(self.time > 0)
			{
				setTimeout(function()
				{
					ctx.fillStyle = col;
					ctx.font = fnt;
					ctx.fillText(self.text, self.x + xoffset, (self.y - yoffset) - move);
					move += 0.1;
				}, self.time);

				self.time-=10;
			}



		}
		return self;
	}
	*/
	
	socket.on("deathCounter", function(data)
	{
		ctx.font = "50px Arial";
		var val = data.value;
		setInterval(function()
		{
			if (Player.list[selfId].canMove == false && val > 0)
			{

				$("#respawnL").text("Respawn in: " + val + " seconds! ");
				val-=1;
				if (val == 0)
				{
					$("#respawnL").text("");
					socket.emit("setCanMove", {playerId:selfId, value:true});
				}
			}
			
		}, 1000);
		
		

	});


	var Bullet = function(initPack)
	{
		var self = {};
		self.id = initPack.id;
		self.x = initPack.x;
		self.y = initPack.y;
		self.map = initPack.map;
		self.roomId = initPack.roomId;
		self.draw = function()
		{
			if (Player.list[selfId].roomId !== self.roomId)
				return;
			ctx.fillStyle = "black";
			var bx = self.x - Player.list[selfId].x + WIDTH/2;
			var by = self.y - Player.list[selfId].y + HEIGHT/2;


			ctx.fillRect(bx-5, by-5, 10, 10);
			ctx.fillStyle = "red";
			//ctx.arc(self.x-5, self.y-5, 5, 0,d 2 * Math.PI, false);
		}

		Bullet.list[self.id] = self;
		return self;
	}
	Bullet.list = {};

	var selfId = null;

	socket.on('init', function(data)
	{

		if (data.selfId)
		{
			selfId = data.selfId;
		}
		for (var i = 0; i < data.player.length; i++)
		{
			new Player(data.player[i]);
			
			//console.log("init");
			//console.log(data.player[i]);
		}
		for (var i = 0; i < data.bullet.length; i++)
		{
			new Bullet(data.bullet[i]);
		}
		
	});

	//Update

	socket.on('update', function(data)
	{
		for (var i = 0; i < data.player.length; i++)
		{
			var pack = data.player[i];
			var p = Player.list[pack.id];
			if (p)
			{
				if (pack.x !== undefined)
				{
					p.x = pack.x;
				}
				if (pack.y !== undefined)
				{
					p.y = pack.y;
				}
				if (pack.hp !== undefined)
				{
					p.hp = pack.hp;
				}
				if (pack.hpMax !== undefined)
				{
					p.hpMax = pack.hpMax;
				}
				if (pack.exp !== undefined)
				{
					p.exp = pack.exp;
				}
				if (pack.expMax !== undefined)
				{
					p.expMax = pack.expMax;
				}
				if (pack.level !== undefined)
				{
					p.level = pack.level;
				}
				if (pack.kills !== undefined)
				{
					p.kills = pack.kills;
				}
				if (pack.deaths !== undefined)
				{
					p.deaths = pack.deaths;
				}
				if (pack.gold !== undefined)
				{
					p.gold = pack.gold;
				}
				if (pack.user !== undefined)
				{
					p.user = pack.user;
				}
				if (pack.latency !== undefined)
				{
					p.latency = pack.latency;
				}
				if (pack.isShielding !== undefined)
				{
					p.isShielding = pack.isShielding;
				}
				if (pack.mouseAngle !== undefined)
				{
					p.mouseAngle = pack.mouseAngle;
				}
				if (pack.shield !== undefined)
				{
					p.shield = pack.shield;
				}
				if (pack.stats !== undefined)
				{
					p.stats = pack.stats;
				}
				if (pack.updatedX !== undefined)
				{
					p.updatedX = pack.updatedX;
				}
				if (pack.updatedY !== undefined)
				{
					p.updatedY = pack.updatedY;
				}
				if (pack.isGoal !== undefined)
				{
					p.isGoal = pack.isGoal;
				}
				if (pack.canMove !== undefined)
				{
					p.canMove = pack.canMove;
				}
				if (pack.elementType !== undefined)
				{
					//console.log(pack.elementType);
					p.elementType = pack.elementType;
				}
				if (pack.maxSpd !== undefined)
				{
					p.movementSpd = pack.maxSpd;
				}
				if (pack.sprite !== undefined)
				{
					p.sprite = pack.sprite;
				}
				if (pack.spriteShield !== undefined)
				{
					p.spriteShield = pack.spriteShield;
				}
			}
		}
		for (var i = 0; i < data.bullet.length; i++)
		{
			var pack = data.bullet[i];
			var b = Bullet.list[data.bullet[i].id];
			if (b)
			{
				if (pack.x !== undefined)
				{
					b.x = pack.x;
				}
				if (pack.y !== undefined)
				{
					b.y = pack.y;
				}
			}
		}
	});

	/*socket.on("matchingLabel", function()
	{
		matchLabel.innerHTML = "Searching...";
	});*/
	//Remove
	socket.on('remove', function(data)
	{
		for (var i = 0; i < data.player.length; i++)
		{
			delete Player.list[data.player[i]];
		}
		for (var i = 0; i < data.bullet.length; i++)
		{
			delete Bullet.list[data.bullet[i]];
		}
	});
	function getStore(team)
	{
		switch(team)
		{
		case "red":
			var val = 1;
		break;
		case "blue":
			var val = 2;
		break;
		}
		for (var x = 0; x < MapGrid1.length; x++)
		{
			for (var y = 0; y < MapGrid1[x].length; y++)
			{
				
				if (MapGrid1[x][y] == val)
				{
					return {x, y};
				}

			}
		}
		return false;

	}


	var drawScore = function()
	{

		killL.innerHTML = "Kills: " + Player.list[selfId].kills + " / ";
		deathL.innerHTML = "Deaths: " + Player.list[selfId].deaths;
		//ctx.fillText("Shield: " + Player.list[selfId].shield, 200, 30);
		goldLabel.innerHTML = Player.list[selfId].gold;
		levelL.innerHTML = Player.list[selfId].level;
		//killL.innerHTML = "Goal: " + Player.list[selfId].isGoal + " / ";
	}

	socket.on("showCombat", function(data)
	{
		var text = null;
		var col = null;
		var fnt = null;
		switch (data.type)
			{
				case "damage":
					text = "-" + data.amount;
					fnt = "15px Arial";
					col = "red";
				break;
				case "crit":
					text = "#" + data.amount;
					fnt = "20px Arial";
					col = "#b30000";
				break;
				case "heal":
					text = "+" + data.amount;
					fnt = "15px Arial";
					col = "green";
				break;
			}
		var playerHit = data.playerHit;
		if (playerHit !== null && selfId !== null && text !== null && col !== null && fnt !== null)
		{

		//var xx = playerHit.x - Player.list[selfId].x + WIDTH/2;
		//var yy = playerHit.y - Player.list[selfId].y + HEIGHT/2;
		//chatText.innerHTML += "x: " + xx + "; y: " + yy + "<br />";
		var d = new Damage(
		{
		x:playerHit.x,
		y:playerHit.y,
		type:data.type,
		amount:data.amount
		});
		d.draw();
		//chatText.innerHTML += playerHit.user + ": " + data.type + "; " + data.amount + "<br />";

		//var d = Damage();
		//d.setAllValues(data);
		//d.draw();

		//step = 0;
		//steps = 100;
		//chatText.innerHTML += amount;
		//runText(amount);
		}
	});

	var Damage = function(initPack)
	{
		var self = {};
		//var playerHit = initPack.playerHit;
		self.x = initPack.x;
		self.y = initPack.y;
		self.type = initPack.type;
		self.amount = initPack.amount;
		self.time = 400;
		self.draw = function()
		{
			if (!selfId)
				return;
			var xx = self.x - Player.list[selfId].x + WIDTH/2;
			var yy = self.y - Player.list[selfId].y + HEIGHT/2;
			var col = null;
			var fnt = null;
			var text = null;
			switch (self.type)
			{
				case "damage":
				text = "-" + self.amount.toFixed(2);
					fnt = "15px Arial";
					col = "red";
				break;
				case "crit":
					text = "#" + self.amount.toFixed(2);
					fnt = "20px Arial";
					col = "#b30000";
				break;
				case "heal":
					text = "+" + self.amount.toFixed(2);
					fnt = "15px Arial";
					col = "green";
				break;
			}

			var yoffset = 80 + genRandomNumber(-10, 10);
			var xoffset = genRandomNumber(-20, 20);
			//var amount = data.amount;

			var move = 0;
			//chatText.innerHTML += "X: " + self.x + "; Y: " + self.y + "<br />";
			while(self.time > 0)
			{
				setTimeout(function()
				{
					ctx.fillStyle = col;
					ctx.font = fnt;
					ctx.fillText(text, xx + xoffset, (yy - yoffset) - move);
					move += 0.05;
				}, self.time);

				self.time-=10;
			}

		}
		return self;
	}

	function genRandomNumber(min, max)
	{
		return Math.random() * (max - min) + min;
	}
	var armorValue = null;
	socket.on("updateArmor", function(data)
	{
		armorValue = data.value;
		updateStatBoard();
	});
	function updateStatBoard()
	{
		if(selfId)
		{
			var stats = Player.list[selfId].stats;
			if (armorValue == null)
			{
				var armor = "Undetermined ";
			}
			else
			{
				var armor = armorValue.toFixed(2) * 100;
			}
			var damage = (stats.attack * stats.attack) / (stats.attack + 0);
			var attackSpdMs = (40 * (5 + stats.attackSpd))
			var attackSpdSec = attackSpdMs / 1000;
			
			var critDam = ((damage * 150) / 100);
			var extraDam = critDam * (stats.critDam / 100);
			
			var critDif = (critDam + extraDam) - damage;
			
			var movementSpd = Player.list[selfId].movementSpd;
			var mapTime = ((3200 / movementSpd) * 40) / 1000;
			mapTime = mapTime.toFixed(2);
			
			$('#attackL').text(stats.attack);
			attackTT.textContent = "You deal " + damage + " damage! (" + critDam + ") against weak elements";
			$('#armorL').text(stats.armor);
			armorTT.textContent = "You took " + armor + "% of damage on the last hit!";
			
			var lethalityPercent = ((stats.armor - stats.lethality) / stats.armor) * 100;
			$('#lethalityL').text(stats.lethality);
			lethalityTT.textContent = "You negate " + lethalityPercent + "% of the enemies armor";
			
			$('#attackSpdL').text(stats.attackSpd);
			attackSpdTT.textContent = "1 Bullet every " + attackSpdSec + " second(" + attackSpdMs + "Ms)";

			$('#critL').text(stats.crit);
			critTT.textContent = "You deal " + (critDam + extraDam) + " damage on critical hit (+" + critDif + ") | +" + extraDam;
			
			$('#lifeStealL').text(stats.lifeSteal);
			var lifeStealAmt = Player.list[selfId].hpMax * (stats.lifeSteal / 100);
			lifeStealTT.textContent = "You heal +" + stats.lifeSteal + "% (+" + lifeStealAmt +") of your health on attack";
			
			var lifeRegenExtra = 3 * (stats.lifeRegen / 100);
			var lifeRegen = 3 + lifeRegenExtra;
			$('#lifeRegenL').text(lifeRegen);
			lifeRegenTT.textContent = "You heal +" + lifeRegen + " hp (+" + stats.lifeRegen +"%) every three seconds!";
			
			$('#movementL').text(movementSpd);
			movementTT.textContent = "You can travel across the map in " + mapTime + " seconds!";
			
		}

	}
	setInterval(function()
	{
		startTime = Date.now();
		if (selfId)
		{
			socket.emit('updateGold', {amount: 1, playerId: selfId, type:"up"});
		}

		socket.emit('calculateLatency');
		updateStatBoard();
	}, 2000); //Calculate Latency

	socket.on('displayLatency', function()
	{
		if(selfId)
		{
			Player.list[selfId].latency = Date.now() - startTime;

		}

	});
	socket.on("updateHealthbar", function(data)
	{
		if(selfId)
		{
			var p = Player.list[selfId];
			if (data.type == "damage")
			{
				var missing = p.hp - data.amount;
				var percent = (missing / p.hpMax) * 100 + "%";
				$('#healthL').text(missing.toFixed(2) + " (-" + data.amount.toFixed(2) + ")");
			}
			else if (data.type == "heal")
			{
				var missing = p.hp + data.amount;
				if (missing > p.hpMax)
				{
					missing = p.hpMax;
				}
				var percent = (missing / p.hpMax) * 100 + "%";
				$('#healthL').text(missing.toFixed(2) + " (+" + data.amount.toFixed(2) + ")");
			}
			//console.log(data.damage);
			$('#healthDiv').css("width", percent);

		}
	});
	function updateExpBar()
	{
		var p = Player.list[selfId];
		var percent = (p.exp / p.expMax) * 100 + "%";
		$('#expDiv').css("width", percent);
		$("#expL").text(p.exp);
		$("#expMaxL").text(p.expMax);
		
	}

	var drawMap = function()
	{

		if (!selfId)
			return; 
		var x = WIDTH/2 - Player.list[selfId].x;
		var y = HEIGHT/2 - Player.list[selfId].y;
		switch(Player.list[selfId].map)
		{
			case "map1":
				ctx.drawImage(Img.map1, x, y);
			break;
			case "map2":
				ctx.drawImage(Img.map2, x, y);
			break;
			case "map3":
				ctx.drawImage(Img.map3, x, y);
			break;
		}


	}
	function getDis(x1, y1, x2, y2)
	{
		let xDistance = x2 - x1;
		let yDisance = y2 - y1;
		return Math.sqrt(Math.pow(xDistance, 2) + Math.pow(yDisance, 2));
	}
	setInterval(function()
	{
		if(!selfId)
			return;
		var p = Player.list[selfId];
		if (p.hp < p.hpMax)
		{
			var healExtra = 3 * (p.stats.lifeRegen / 100);
			var healAmt = 3 + healExtra;
			socket.emit("increaseHP", {amount: healAmt, playerId:selfId});
		}
	}, 3000);

	playerInventory = Inventory();
	
	loadStore();
	function loadStore()
	{
		storeDiv.innerHTML = "";
		for (var i in Element.list)
		{
			storeDiv.innerHTML += "<button id='btn"+Element.list[i].name+"'  onclick=selectElement('"+i+"')>" + Element.list[i].name + "</button><br /><label>" + Element.list[i].ability + "</label><br /><label>Strong against: " + Element.list[i].strength + "</label><br />	<label>Weak against: " + Element.list[i].weakness + "</label><br />";
		}
	}
	
		
	function selectElement(itemId)
	{
		if (checkStoreRange())
		{
			for (var ii in Player.list)
			{
				var p = Player.list[ii];
				if (p.team == Player.list[selfId].team)
				{
					if(p.elementType == Element.list[itemId].name)
					{
						return;
					}	
				}
				
			}
			socket.emit("setElement", {playerId:selfId, elementType:Element.list[itemId].name});
			
			
			$("#elementL").text(Element.list[itemId].name);
			Element.list[itemId].event();
			storeDiv.innerHTML = "";
			for (var i in Item.list)
			{
		
				storeDiv.innerHTML += "<button onclick=buyItem('"+i+"')>" + Item.list[i].name + ": " + Item.list[i].gold + " Gold </button><br /><label>" + Item.list[i].explain + "</label><br /><hr />";
			}
		}
		
	}
	socket.on("disableElement", function(data)
	{
		var buttonId = "#btn" + data.elementType;
		$(buttonId).prop("disabled", "true");
	});
				
	function buyItem(itemId)
	{
		if (checkStoreRange())
		{
			//console.log(Item.list[itemId].type);
			if (itemId == "feather" && Player.list[selfId].stats.attackSpd - 0.2 < 0)
			{
				return;
			}
			else
			{
				playerInventory.addItem(itemId, 1, Item.list[itemId].gold, Item.list[itemId].type);
			}
			
		}

	}
	function checkStoreRange()
	{
		if (!selfId)
			return;
		var p = Player.list[selfId];
		var x = getStore(p.team).x * TILESIZE;
		var y = getStore(p.team).y * TILESIZE;
		console.log(getDis(x, y, p.x, p.y));

		if (getDis(x, y, p.x, p.y) <= 416)
		{
				return true;
		}
		else
		{
			return false;
		}

	}
	setInterval(function()
	{
		if (selfId == null)
			return;

		//checkStoreRange();

		ctx.clearRect(0, 0, 1200, 1000);
		drawMap();
		drawScore();
		updateExpBar();
		//drawName();
		//updateScoreBoard();
		ctx.fillStyle = "black";
		ctx.fillRect(entryCoor.x, entryCoor.y, 5, 5);
		if (Player.list[selfId].isShielding)
		{
			socket.emit('updateShield', {state:false}); //True for positive, false for negative
		}
		else
		{
			socket.emit('updateShield', {state:true});
		}

		//chatText.innerHTML += "cleared";
		for (var i in Player.list)
		{
			Player.list[i].draw();
		}
		for (var i in Bullet.list)
		{
			Bullet.list[i].draw();
		}

	}, 40);

	socket.on('getPlayerAngle', function(data)
	{
		Player.list[selfId].mouseAngle = data.mouseAngle;
	});

	socket.on('addToChat', function(data)
	{
		
		console.log(data.name + ";" + data.txt);
		chatText.innerHTML += '<div><b>' + data.name + '</b>' + data.txt + '</div>';
	});
	socket.on('evalAnswer', function(data)
	{
		console.log(data);
	});
	socket.on('updateScoreBar', function(data)
	{

		//$("#team1L").text("(+" + data.team1N + ")");
		//$("#team2L").text("(+" + data.team2N + ")");

		$("#team1Div").css("height", data.team1);
		$("#team2Div").css("height", data.team2);
	});


	chatForm.onsubmit = function(e)
	{
		e.preventDefault();
		if (chatInput.value[0] === '/')
		{
			socket.emit('evalServer', chatInput.value.slice(1));
		}
		else if (chatInput.value[0] === '@')
		{
			socket.emit('sendPMToServer', {
				user: chatInput.value.slice(1, chatInput.value.indexOf(',')),
				message: chatInput.value.slice(chatInput.value.indexOf(',') + 1)
			});
		}
		else
		{
			socket.emit('sendMsgToServer', chatInput.value);
		}
		
		chatInput.value = '';
	}

	document.onkeydown = function(event)
	{
		if(event.keyCode === 68) //D
		{
			socket.emit('keyPress', {inputId: 'right', state:true});
		}
		else if(event.keyCode === 83) //S
		{
			socket.emit('keyPress', {inputId: 'down', state:true});
		}
		else if(event.keyCode === 65) //A
		{
			socket.emit('keyPress', {inputId: 'left', state:true});
		}
		else if(event.keyCode === 87) //W
		{
			socket.emit('keyPress', {inputId: 'up', state:true});
		}
		else if(event.keyCode === 32) //Space
		{

			socket.emit('keyPress', {inputId: 'shield', state:true});
		}
	}
	document.onkeyup = function(event)
	{
		if(event.keyCode === 68) //D
		{
			socket.emit('keyPress', {inputId: 'right', state:false});
		}
		else if(event.keyCode === 83) //S
		{
			socket.emit('keyPress', {inputId: 'down', state:false});
		}
		else if(event.keyCode === 65) //A
		{
			socket.emit('keyPress', {inputId: 'left', state:false});
		}
		else if(event.keyCode === 87) //W
		{
			socket.emit('keyPress', {inputId: 'up', state:false});
		}
		else if(event.keyCode === 32) //Space
		{

			socket.emit('keyPress', {inputId: 'shield', state:false});
		}
		else if(event.keyCode === 49) //1
		{
			console.log ("1");
			if (!selfId)
				return;
			//socket.emit('keyPress', {inputId: 'item1', state:true});
			if (playerInventory.items[0] !== undefined)
				
				Item.list[playerInventory.items[0].id].event();
		}
		else if(event.keyCode === 50) //2
		{
			console.log ("2");
			if (!selfId)
				return;
			if (playerInventory.items[1] !== undefined)
				Item.list[playerInventory.items[1].id].event();
			//socket.emit('keyPress', {inputId: 'item1', state:true});
		}
		else if(event.keyCode === 51) //3
		{
			console.log ("3");
			if (!selfId)
				return;
			if (playerInventory.items[2] !== undefined)
				Item.list[playerInventory.items[2].id].event();
			//socket.emit('keyPress', {inputId: 'item1', state:true});
		}
	}

	document.onmousedown = function(event)
	{
		//chatText.innerHTML += event.clientX + ":" + event.clientY + "<br />";

			socket.emit('keyPress', {inputId: 'attack', state: true});


	}
	document.onmouseup = function(event)
	{
		socket.emit('keyPress', {inputId: 'attack', state: false});
	}
	document.onmousemove = function(event)
	{
		/*var x = -600 + event.clientX - 8;
		var y = -500 + event.clientY - 8;
		var angle = Math.atan2(y, x) / Math.PI * 180;*/
		if (pointerLocked == true)
		{
			
			socket.emit('keyPress', {inputId:'mouseAngle', xx:entryCoor.x, yy:entryCoor.y});
		}
		

	}
	document.oncontextmenu = function(event)
	{
		event.preventDefault();
	}
