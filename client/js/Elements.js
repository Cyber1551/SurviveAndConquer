//*******Elements**********
Element = function(name, ability, strength, weakness, event, lore)
{
	var self =
	{
		name:name,
		ability:ability,
		strength:strength,
		weakness:weakness,
		event:event,
		lore:lore
	}
	//console.log(name);
	Element.list[self.name] = self;
	return self;
}
Element.list = {};


Element("Water", "+500 Max Health", "Fire", "Lightning", function()
{
		
	socket.emit("updateMaxHp", {playerId:selfId, amount:500, type:"up"});
}, "Water mages are kind and<br>gentle. They are all about life and<br>peace!");
Element("Lightning", "+25% Critical Chance", "Water", "Wind", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"crit", type:"up", amount:25});
}, "Lightning mages are brutal.<br>Their extra damage makes them<br>great assassins for picking off the weak!");
Element("Earth", "+10 Armor", "Wind", "Fire", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"armor", type:"up", amount:10});
}, "Earth mages are tough and<br>vigilant. They fulfill their duties<br>head on and are usually the front lines.");
Element("Fire", "+10 Attack Damage", "Earth", "Water", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attack", type:"up", amount:10});
}, "Fire mages value destruction<br>and cruelty. They are the fighters<br>that destroy their enemies.");
Element("Wind", "+1 Attack Spd", "Lightning", "Earth", function()
{
	socket.emit("updateStats", {playerId:selfId, stat:"attackSpd", type:"up", amount:1});
}, "Wind mages value knowledge<br> and control. They rely on taking<br>out the enemy as fast as possible.");

