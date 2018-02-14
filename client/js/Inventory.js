
Inventory = function()
{
	var self = {
		items:[], //{id:"itemId", amount:1}
		passive:[]
	}
	self.addItem = function(id, amount, gold, type)
	{
		if (Player.list[selfId].gold >= gold)
		{

			for (var i = 0; i < self.items.length; i++)
			{
				if (type == "active")
				{
					if (self.items[i].id === id)
					{
					//Player.list[selfId].gold -= gold;
						socket.emit('updateGold', {amount: gold, playerId: selfId, type:"down"});
						self.items[i].amount += amount;
						self.refreshRender();
						return;
					}

				}


			}
			for (var i = 0; i < self.passive.length; i++)
			{

				if (type == "passive")
				{
					if (self.passive[i].id === id)
					{
						socket.emit('updateGold', {amount: gold, playerId: selfId, type:"down"});
						self.passive[i].amount += amount;
						Item.list[id].event();
						self.refreshRender();
						return;
					}

				}

			}
			if (type == "active")
			{
				socket.emit('updateGold', {amount: gold, playerId: selfId, type:"down"});
				self.items.push({id:id, amount:amount});
			}
			if (type == "passive")
			{

				socket.emit('updateGold', {amount: gold, playerId: selfId, type:"down"});
				self.passive.push({id:id, amount:amount});
				Item.list[id].event();
			}

			//self.items.push({id:id, amount:amount});
			self.refreshRender();
		}

	}
	self.removeItem = function(id, amount)
	{
		for (var i = 0; i < self.items.length; i++)
		{
			if (self.items[i].id === id)
			{
				socket.emit('updateGold', {amount: (Item.list[id].gold / 2), playerId: selfId, type:"up"});
				if (Item.list[id].type == "active")
				{

					self.items[i].amount -= amount;
					self.refreshRender();
					if(self.items[i].amount <= 0)
					{
						self.items.splice(i, 1);
						self.refreshRender();
						return;
					}
				}
				else if(Item.list[id].type == "passive")
				{
					self.passive[i].amount -= amount;
					self.refreshRender();
					if(self.passive[i].amount <= 0)
					{
						self.passive.splice(i, 1);
						self.refreshRender();
						return;
					}

				}


			}
		}
	}
	self.hasItem = function(id, amount)
	{
		for (var i = 0; i < self.items.length; i++)
		{
			if (self.items[i].id === id)
			{
				return self.items[i].amount >= amount;
			}
		}
		return false;
	}
	self.refreshRender = function()
	{
		var str = "<b>Active Items</b><br />";

		for (var i = 0; i < self.items.length; i++)
		{
			let item = Item.list[self.items[i].id];
			//let use = "Item.list['" + item.id + "'].event()";
			str += item.name + " x" + self.items[i].amount + "<br />";
		}
		str += "<b>Passive Items</b><br />";
		for (var i = 0; i < self.passive.length; i++)
		{
			let item = Item.list[self.passive[i].id];
			//let use = "Item.list['" + item.id + "'].event()";
			str += item.name + " x" + self.passive[i].amount + "<br />";
		}
		document.getElementById("inventory").innerHTML = str;
	}
	return self;
}


Item = function(id, name, gold, type, event, explain)
{
	var self =
	{
		id:id,
		name:name,
		gold:gold,
		type:type,
		event:event,
		explain:explain
	}
	Item.list[self.id] = self;
	return self;
}
Item.list = {};


//Active Items

Item("potion", "Potion", 40, "active", function()
{

	playerInventory.removeItem("potion", 1);
	socket.emit("increaseHP", {amount: 100, playerId:Player.list[selfId].id});

}, "+100 health on use!");

Item("overclock", "OverClock", 200, "active", function()
{
	if (playerInventory.hasItem("overclock", 2) == false && Player.list[selfId].overclocked == false && Player.list[selfId].stats.armor >= 10)
	{
		playerInventory.removeItem("overclock", 1);
		Player.list[selfId].overclocked = true;
		socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"up", amount:20});
		socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"down", amount:10});
		setTimeout(function()
		{
			socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"down", amount:20});
			socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"up", amount:10});
			Player.list[selfId].overclocked = false;
		}, 10000);
	}
	

}, "**Limit 1<br />+20 Attack Damage!<br />-10 Armor!<br />Lasts 10 Seconds");


//Passive Items

Item("boots", "Basic Boots", 50, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"movement", type:"up", amount:1});

}, "**Limit 1<br />+1 Movement Speed");

Item("basicattackGem", "Basic Attack Gem", 65, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"up", amount:10});

}, "+10 Attack Damage!");

Item("mediumattackGem", "Medium Attack Gem", 140, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"up", amount:25});

}, "+25 Attack Damage!");

Item("supplybelt", "Supply Belt", 70, "passive", function()
{

	socket.emit("updateMaxHp", {playerId:selfId, amount:100, type:"up"});

}, "+100 Maximum Health!");

Item("gsupplybelt", "Giant Supply Belt", 130, "passive", function()
{
	socket.emit("updateMaxHp", {playerId:selfId, amount:200, type:"up"});

}, "+200 Maximum Health!");

Item("basicarmor", "Basic Armor", 50, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"up", amount:5});

}, "+5 Armor!");

Item("mediumarmor", "Medium Armor", 135, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"up", amount:15});

}, "+15 Armor!");
Item("basiclethality", "Basic Lethality", 200, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"lethality", type:"up", amount:5});

}, "+5 Armor Penetration!");

Item("mediumlethality", "Medium Lethality", 375, "passive", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"lethality", type:"up", amount:10});

}, "+10 Armor Penetration!");

Item("feather", "Feather", 75, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"attackSpd", type:"up", amount:0.5});

}, "+0.5 Attack Speed");

Item("basicelectricgem", "Basic Electric Gem", 80, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"crit", type:"up", amount:10});

}, "+10 Critical chance"); 

Item("mediumelectricgem", "Medium Electric Gem", 230, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"crit", type:"up", amount:40});

}, "+30 Critical chance");
Item("largeelectricgem", "Large Electric Gem", 350, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"crit", type:"up", amount:25});
	socket.emit("updateStats", {playerId:selfId, stat:"critDam", type:"up", amount:50});

}, "+25 Critical chance<br />+50% bonus critical damage");

Item("basichealinggem", "Basic Healing Gem", 175, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"lifeSteal", type:"up", amount:1});

}, "+1% Life Steal"); 

Item("mediumhealinggem", "Medium Healing Gem", 500, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"lifeSteal", type:"up", amount:3});

}, "+3% Life Steal");
Item("largehealinggem", "Large Healing Gem", 500, "passive", function()
{

	socket.emit("updateStats", {playerId:selfId, stat:"lifeRegen", type:"up", amount:200});

}, "+200% Life Regen!");



