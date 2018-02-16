//*******Elements**********
Element = function(name, ability, strength, weakness, event)
{
	var self =
	{
		name:name,
		ability:ability,
		strength:strength,
		weakness:weakness,
		event:event,
	}
	console.log(name);
	Element.list[self.name] = self;
	return self;
}
Element.list = {};


Element("Water", "+150 Max Health", "Fire", "Lightning", function()
{
		
	socket.emit("updateMaxHp", {playerId:selfId, amount:150, type:"up"});
});
Element("Lightning", "+25% Critical Chance", "Water", "Wind", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"crit", type:"up", amount:25});
});
Element("Earth", "+10 Armor", "Wind", "Fire", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"up", amount:10});
});
Element("Fire", "+10 Attack Damage", "Earth", "Water", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"up", amount:10});
});
Element("Wind", "+1 Attack Spd", "Lightning", "Earth", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attackSpd", type:"up", amount:1});
});

