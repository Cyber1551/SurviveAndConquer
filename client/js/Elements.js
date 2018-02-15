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


Element("Water", "+100 Max Health", "Fire", "Lightning", function()
{
	
	socket.emit("updateMaxHp", {playerId:selfId, amount:100, type:"up"});
});
Element("Lightning", "+25% Critical Chance", "Water", "Wind", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"crit", type:"up", amount:25});
});
Element("Earth", "+10 Armor", "Wind", "Fire", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"up", amount:10});
});
Element("Fire", "+5 Attack Damage", "Earth", "Water", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"up", amount:5});
});
Element("Wind", "+1.5 Attack Spd", "Lightning", "Earth", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attackSpd", type:"up", amount:1.5});
});

