# Project Bluprint
(Use soft-wrap for unformatted reading)

This document is focused entirely on the programming part of the development, so it will be an overview of logic and structure of each feature of the game, and how to connect all parts. It focuses on features abstraction, but it might be expanded with time to cover specificities.

## Branching

---

This covers the workflow of the project, specifically regards the decision on what to work on. Every section of the game can have a certain quantity of active branches to avoid a massive and overly segmented parallel working. 

* **UI:** 1 active branch;
* **Map:** 1 active branch;
* **Entities:** 2 active branches;
* **Interactions:** 2 active branches;
* **Game modes:** 1 active branch;
* **Storage:** 1 active branch;

In addition to these branches, there are two administrative (or leading) branches: **dev** and **main**. Any of the section branches should be created from dev, and merged on it after completion, and no one of the branches should change elements of other features. These are the active branches:

**Leading:**        **Main | Dev**	
**Map:**            Inactive	
**Entities:**       Inactive	
**Interactions:**   Inactive	
**Game Modes:**	    Inactive
**Storage:**        Inactive

## Map

---

The map is where the players move, buildings are placed, units interact, etc. It’s the field where the game happens. This section encompasses every aspect related to the game map: its structure, generation, functionality, and more. Within this section, two primary features are distinguished: **map structure** and **map generation**.

### Map Structure

Refers to how the map will be made up, the underlying mechanisms of map topology, and its elements. The game will be made on a bidimensional perspective (2d), containing four layers:

* **Background:** Is a mask for the map that doesn’t let the player see the “void” behind the main game’s content. It can be made through an image fixed to the camera’s position.
* **Ocean:** Made for units and buildings that exist on water.
* **Ground:** Made for units and building that exist on ground.
* **Sky:** Made for units that move on air.

Each layer will be made of tiles, which have these properties: **occupied**, **walkable**, **visible**, **size**, **position**, **id** (layer + coordinates), **environment**, and **sprite**. There will be 4 environments, which have their own properties: jungle, forest, darklands, and plains.

### Map Generation

Maps will have two types of generation, but both will be stored in the same way, so after a piece of the map is generated, its information is permanent. The first generation type is **automatic** generation. It uses player position, and AI to procedurally generate the map, mixing randomness with other parameters to create a fair environment for the players. This method is the default on skirmish mode, but it is possible to change it. The second method is **custom** map generation. Campaign maps and custom maps use this method. Custom maps are made tile by tile, but some in-game tools can be used to make this process faster. 

## Entities

---

Distinguished from structural entities present in each feature, this section encompasses all game-related entities—units, heroes, items, buildings, etc. However, an initial point of potential confusion lies in the distinction between system entities (such as weather, time, and physics engines) and game entities. Within this section, the focus is on the business rules of the project, omitting system entities while addressing game entities.

Game entities can be categorized into two types: concrete entities and conceptual entities. The concrete entities feature a range of elements including: **buildings**, **heroes**, **items**, **resources**, **units**, and **civilizations**. Conversely, conceptual entities encapsulate features like: appearance, researches, and stats attributes.

Concrete features are often more intricate, potentially utilizing conceptual entities or even other concrete entities as properties or components.

This categorization serves to clarify the distinction between these entity types, establishing a framework that aids in understanding and organizing various components within the game environment.

### Entities Assembly

Given that conceptual entities, like stats, aren’t inherited by a single feature, and should be loosely coupled, the decision on where to store and how to gather an entity’s properties is very important. It can’t be stored on units, heroes, buildings, etc., features itself, because that would make them thightly coupled to those features. There are then two options: storing specific properties on the feature of that property directory itself through ids, or creating a database that uses ids to store any unit information. The second option is better, because the first option hurts SCA principles of abstracting business rules and use cases. This way, any specific details for units could be handled fast and without much intermediate operations, while still keeping everything loosely coupled. That database will work like an assembly for any units.

### Appearance (Conceptual Entity)

Appearance feature works like a secondary database for entities assembly, but with its own entities and use cases for visual aspects of the game. When a unit is created for example, if we want to randomize its body part colors, we need to know which parts will be randomized, and even what visual parts it has. Knowing that, appearance feature has informations regards parts of entities that will be created that can vary between entities. 

Since the intention here is to not change features, only its implementation when making patches, adding other features, etc., appearance feature works as a way for appearance informations not to be needed on other features. Let’s say a request to create a Kinhnami is sent to Entities Assembly. It will request the needed informations for an interactor, including appearance informations, like number of **body parts**, **colors** to use, etc. This is particularly useful for customization. Remember, however, that just like other features, appearance works through abstraction and generalizations, so appearance feature doesn’t know what a Kinhnami unit is. But the assembly does and use the proper informations to create it.

### Stats Attributes (Conceptual Entity)

Statistical attributes are measurable information that is inherited by units and heroes. They define different aspects of gameplay, and allow a diverse range of strategies to the game. Stats attributes have different effects, which are described on the game wiki. There are five unique attributes: **strength**, **intelligence**, **constitution**, **dexterity**, and **charisma**.

### Researches (Conceptual Entity)

Researches are special upgrades of certain buildings that take some time to be activated. Each research has some general informations, like **research time**, and **effects**, and are separated in different types: **race advancements**, **statistic boost**, **resource generation boost**, **unit unlock**, and **ability unlock**. Since all researches are pre-made, they can be directly stored instead of needing the entity assembly in order to work, however their effects application depends directly on interaction features. However, it is possible to use entities assembly to create researches the same way it is done with other entities. This is a bonus that make researches customizable by player if it is wanted at some point of the project. 

Something important about researches is that it is separated between two features, one for research entities (its information), and researching interaction, which refers to player interaction with research entities.   

### Resources (Concrete Entity)

Resources are quantitative entities that serve as game currencies for other process, like building, training other units, researching, etc. Resources feature houses all types of resources and their information, as well as their generation structures. There are five types of resources: **food**, **wood**, **metal**, **stone**, and **crystals**. It is important to note that resources feature itself doesn’t cover their use, only their existence and generation. The method to store and access them is something specific, and not abstract, but it could be stored on match variables, for example, which would let them to be accessible by any feature easily (respecting class protection, of course).

### Structures (Concrete Entity)

Structures are immovable entities. They have two different types, which distinction is very important. All structures are interactable, but not all of them are buildable, and that’s what distinct **Buildings** from **Natural Structures**. Both these types also have subclasses with their respective informations, but to avoid concreteness on the feature, the subclasses will be treated as **presets** for Entities Assembly to understand what it needs to do for each type of structure. Some informations are existing on all structures, like **size**, and **position**.

#### Buildings

Structures that can be built are buildings. They vary depending on races, civilizations, have **cost**, and many effects. There are two types of buildings and many classes. **Race buildings**, and **towers/walls** are the main types of buildings. Building classes are based on their function, and are: **production**, **economy**, **research**, **headquarters**, **defensive**, **population**, and **unusual**. 

Their information on features must be abstract and strict to the feature, which means that the feature doesn’t know what are its effects on the game and other features, like units, map, etc. The only effect it will know are abstractions of effects. ProduceUnit1() function, for example might be an information contained on it, but it doesn’t really know what unit it is producing, it is simply a call to create a unit, which also requests information like unit cost, and nothing more. The unit creation itself doesn’t exist on the feature. The same applies for the next subfeature. 

#### Neutral Structures

Neutral structures aren’t buildable, which means they are part of the map in some sense, but their behavior shares many similarities to buildings, and that’s why both are subfeatures of structures feature. Instead of being built, neutral structures can be converted or captured. Their distinction is based on their function as well, and their use are either **resource generation** or **training special units**. For resource generation, there are two types of structures: **resource deposits**, and **resource generators**, both of them having unique structures for each type of resource.

### Units (Concrete Entity)

Units within the game are multifaceted entities, akin to non-playable characters, each possessing a range of distinct properties and interactions with other game features. To streamline their information, unit properties are categorized into two types, and four main groups: **general**, and **specific properties**, based on if a property is common to all units or not, and **innate**, **birth**, **stable**, and **dynamic properties**, based on property stability.

* **General and Specific Properties:** Units possess both **general properties**, universal to all units (such as position, velocity, and fundamental stats attributes), and **specific properties** unique to each unit type. These specific properties encapsulate functions or behaviors exclusive to particular units, delineating their distinct capabilities, like specialized actions such as "Dive()" for aerial units, altering their map layer from sky to ground.
* **Classification by Property Stability:** The categorization into innate, birth, stable, and dynamic properties hinges on their stability and changeability during gameplay. **Innate properties**, set before a unit's creation, remain constant across all instances of that unit type. **Birth properties**, decided upon unit creation, are unalterable thereafter, encompassing fixed characteristics like initial stats and appearance. **Stable properties**, while subject to change during gameplay, do so infrequently, encompassing elements like evolving stats attributes. **Dynamic properties**, in constant flux throughout the game, encompass variables like position, health points, experience, and transient states.

This classification allows for a clear understanding of how unit properties are structured, providing a framework to manage their diverse characteristics within the game environment.

It’s important to understand, though, that most properties used by units are abstractions, and not the properties itself. The units feature is like a blueprint for the Entities Assembly, and a brain for the units to know what to do and what to be. It will have abstractions of informations, and functions for abstractions of their behavior, which requests information from multiple features. For example, a unit must know **when** to attack, but the attack interaction is handled by combat feature, which means they don’t know **how** to attack, they just call the attack function.

Units have types, which are classes that units are assigned with. An unit type will get increased stats against another different unit type such as Spearman getting more damage against Cavalry units.

* **Spearman:** - Basic Anti-Cavalry units with 1-3 Melee range. Strong versus Cavalry, Weak versus Infantry and Ranged.
* **Infantry:** Basic melee units. Strong versus Spearman, Weak versus Cavalry.
* **Ranged:** Basic ranged units that attack from a distance. Strong versus Spearman and Infantry, Weak versus Cavalry.
* **Cavalry:** Units that are on a mount and can move very fast. Strong versus Infantry and Ranged, Weak versus Spearman.
* **Spellcaster:** Units that have magic abilities.
* **Aerial:** Units that can fly and will have to dive down to attack melee units, but this makes them available for melee units to attack them. Strong versus Ground units, Weak versus Ranged.
* **Naval:** Units that move on the water, but cannot move on the land.
* **Siege:** Units that are strong against Buildings.
* **Monster:** Additional Unit type and tag.
* **Commander:** Units that are stronger, tougher, and can convert mines faster then other units.  

### Civilizations (Concrete Entity)

Civilizations are groups of units, buildings, and researches. Each civilization has unique aspects, such as abilities, graphics, etc. In the game codebase, they would be similar to a preset, reuniting the proper entities. The player can choose what civilization to pick, and this will last for at least the match.

#### Factions

In addition to default civilizations, there are factions. Factions are exceptions for civilizations. They work in the same way, but remove some features of original civilizations, and add others. It’s important to note that factions are **not** a subgroup of civilizations. They are exceptions. It’s important to emphasize that, because one of the principles of software architecture is that a child class/object should be able to replace their parent without loss of functionalities or different behavior. Since factions **replace** some of their civilizations aspects, they should be parallel to civilizations. Some options for this are: creating a supergroup for both civilizations and factions (which is a good option, and could use **races** as a base) or copying some of the shared features of civilizations on factions manually. That could be better if there are too many differences between factions and civilizations or if control over their logic is crucial.

#### Adding and Editing

The user must be able to add custom civilizations and edit current ones. This should be easy and clear, and the possibility for player to do such things also make understanding how civilizations is structured on SCA. It’s very simple and straightforward: civilization features won’t have any information about units, researches, etc. There will be only places to store it, and during the creation of civilizations, any specific information will be stored through Entities Assembly. There are also pre-made civilizations, which could or not be created through Entities Assembly, but it is adviceable to create it using Entities Assembly to avoid inconsistency.

### Heroes (Concrete Entity)

Heroes are the main feature of the game. They work similarly to units, but have more information and more interactions, in other words, they are more complex units. For code architecture, they work the same way, having their own types, with properties classified by specifity and stability. They also have more customizable information, like name, appearance, and benefit from items. Just like units, the heroes features work like a preset of functions and properties for the entities assembly to work. After its creation, they can also be customized, and this is requested by UI to the game core. This is something particular to SCA, and is based on the principle of features not knowing their exterior, so heroes feature actually doesn’t know what heroes the player created. 

#### Creation

Hero Creation is made up of **Civilization**, **Species**, **Race**, **Appearance**, **Stats Attributes**, and their **Abilities**.

* **Civilization** - The hero would need to pick an civilization to gain perk choices from that Civilization.
* **Species + Race** - The hero would need to pick an species like Humans, or Elypions(Beetlemen), etc. Then they would need to pick an race for that particular species like Kinhnami for Humans or Longhorns for Elypions.
* **Appearance** - This is the most freeform of the customization. You can customize how your hero will look like in the game.
* **Stats Attributes** - They will be assigned certain points in Strength, Dexterity, Constitution, Intelligence, and Charisma based on their Species.
* **Abilities** - This is where they can pick certain abilities to add into their six ability slots. A Hero can only use up to six different abilities and those abilities cost mana or energy to use unless stated otherwise.

#### Customization

Hero customization is limited. A hero can’t change their species, as that would be very strange for example, but they could change their appearance, name, assigned stats, etc. This uses properties stability to determines what properties are passible to change. Any property that is customizable after hero creation should be labelled as **stable property**, and not **birth property**, despite being choseable during creation using the same process. 

### Items (Concrete Entity)

Collectable and usable objects capable of confering **stats** or new **magic abilities** to the player are named items. In the case of Highborne Battlecry, there aren’t any consumable items, all of them are **equipable** and are stored in the hero’s inventory. Item types are:

* **Head:** helmets, crowns, etc.
* **Body:** armors, chest plates, robes, etc.
* **Accessories** (four miscellaneous items): all jewelry, rings, necklaces, etc.
* **Off Hand** (right and left hand): all banners, trinkets, shields etc.
* **Main Hand** (right and left hand): swords, staffs, maces, spears, axes, shields, banners etc.

Similarly to units, items are seen as presets on its code aspect. Item feature might know what properties, classes, and effects items might have, but not what specific items do. This is work for the entities assembly. 

## Interactions

---

Interactions are process-like features that can be used as complex and abstracted functions executers, and involve more than one features. The intention of having features for interactions is to determine what are the informations used on game process and how these processes work. However, it’s important to remember that interaction features have entities, and use cases, just like any other feature. Despite their dynamic behavior, they still use the SCA structure, but are different from interactor layers. The information flow on interaction features involves a request made from player or the game system, a request for information to internal interactor layers, the processes involving all the information used, and an output for the result.    

### Abilities

Abilities are special interactions that affect the gameplay by giving collective and long span effects. Abilities can be either **active**, being cast or turned on/off by the player, or **passive**, working automatically without the need for input. Abilities can also be either **permanent**, or **long-term**, having or not cooldown to be activated. That is the main difference between abilities and skills, which are punctual, and often only affect a certain area or a limited range of units/building.

The abstraction of abilities covers their **rules**, regards what are their requirements to be active and what mantain them active or disable them, including possible cooldowns, material costs, qualified groups to use them, etc., and **effects**, regards what they do, including affected groups, stats changes, triggered events, special effects, etc. Remember, however, that types, and most properties of this feature are simply placeholders and presets rather than specific implementations itself. Abilities can also have different classes depending on their effects, rules, qualified entities (to use it), etc.

### Building

Player could choose the available building from the tab and place it in an available spot. After placing the construction, all nearby builders will be called to build it. When the construction will be finished, (all builders will head to the next construction or remain without task) construction sprite will change into building and allow the player to use building to train units or develop researches. 

Its structure implementation, while ensuring SCA can be confusing, just like any other interaction feature, but maintaining the information abstract, and loosely coupling it doesn’t mean it can’t know informations, like position, building size, etc., it just means that any of the known informations exist **inside** the function, and any specific information is simply requested. The position, building size, building cost, are passed as parameters, not specific information itself. The feature will always return information, and not implement it. Any application/implementation isn’t made by this feature, but by services, like databases, UIs, APIs, etc.

### Combat 

Combat involves several processes, but mantain a basic structure involving an **attacker**, that requests the game code for the attack process, the **attack interaction**, and the **attacked**. The function of combat feature is to abstract the mediation of the request, and its application, having functions like checking if attack is possible, calculating attack effect, requesting unit update, etc. There are three types of attacks:

* **Melee Combat:** basic type of combat that requires to get to the certain distance to deal damage to the enemy unit.
* **Ranged:** archers, gunners, wizards and some monsters could attack from a distance, although damage is rather lower than melee units and there is a chance to miss bullet / bolt / any ranged attack.
* **Magic:** all abilities that can be used by a hero or a certain units could deal damage once they are used.   

And three categories:

* **Ground**
* **Air**
* **Both**

When the unit finds an enemy object that could be attacked by him, the unit will approach it and start attack it. There are limitations created by the categories for example: some melee ground units could not be able to attack any flying units.

### Conversion

This interaction has two stances: **conversion** and **contestion**. Heroes and units are able to convert structures to their team, which makes it work for them. By getting close to a neutral structure, the hero/unit automatically starts to convert it, which takes some time based on **conversion rate**, which can be increased by the number of converters. However, if an enemy unit/hero gets close to the structure, it starts to be contested, which pauses the conversion, until there is only one team near that building or none. To prevent a structure to be contested, the team must eliminate any enemy unit/hero within the structure **conversion range**.

For its code structure, conversion feature must have abstraction for any information regards conversion, like conversion rate calculation, conversion range, conversion stance, contestion stance, without entering into specific details. For this, the feature can use **empty parameters**, which specifities will be decided by external parts of the code. 

### Events

Events within the game primarily encompass map interactions, such as the day/night cycle and weather conditions. Although these are the current planned events, the structure is designed to accommodate any future additions seamlessly. The logic behind events revolves around three core aspects: **visual effects**, **gameplay impacts**, and **triggers**.

1. **Visual Effects:** Visual implementations tend to be highly specific. Therefore, within the abstraction, the focus remains on defining which visual effect to execute. External services (intermediated by external and internal interactors layers) will retrieve from the feature the necessary properties for the visual effect and an abstract representation of how to execute it.
2. **Gameplay Effects:** The abstraction for gameplay effects centers on logic and unspecified parameters. This straightforward approach allows for the definition of the game's mechanics without detailing specific values or conditions.
3. **Triggers:** Triggers can assume two forms—**punctual** or **permanent**. Punctual triggers enable events based on in-game occurrences or randomness and can occur multiple times or not at all. In contrast, permanent triggers initiate an event cycle at the start of the match/game. The permanence of these triggers can be toggled before the game begins, depending on the selected game mode. Importantly, while permanent triggers persist throughout the game, they can still be influenced by punctual triggers. For instance, players might activate or deactivate the day/night cycle through a unit's ability (a punctual trigger) to temporarily freeze the cycle.

### Scenes

Scenes represent predefined sequences of movements for specific characters within the game. They can either be observed passively or allow partial control by the player, akin to the structure often found in tutorials. Much like an algorithm, scenes consist of sets of instructions but are intentionally designed to be customizable and easily created, functioning as a “**scene creator toolkit**”.

The scenes feature is not meant to store specific scenes but rather to define patterns and properties for scenes. As scenes can encompass a wide range of possibilities, attempting to abstract all potential interactions may not be ideal. Instead, the abstraction focuses on facilitating scene construction. For instance, during scene creation, it should enable the definition of interactions as **frozen** or **inaccessible**, a storage for artificial inputs that **transcend** this inaccessibility, the establishment of **barriers** for player input, and similar tools.

### Leveling

Following each mission, the player accumulates **experience points** based on various factors, such as **defeated enemies**, **trained units**, and **destroyed buildings**. These experience points contribute to character advancement, allowing for level-ups when a sufficient amount is amassed. Leveling up grants **stat points**, offering player-driven customization, alongside additional bonuses.

The abstraction of the leveling feature revolves around the calculations involved in determining level progression. This involves abstracting the **parameters** essential for the calculation, developing a function responsible for these **calculations**, and creating another function that **initiates** the leveling process. Additionally, there's an abstraction for each function to request the execution of subsequent functions, such as adding bonuses.

The feature interacts with specific details housed in other layers by utilizing abstracted placeholders during the calculation process.

### Movement

Movement within the game operates based on four primary properties: **starting point**, **destination**, **map layout**, and the **entities** in motion. This feature responds to calls from either the game's intelligence or player inputs, utilizing these properties to calculate and execute object trajectories over time.

The movement system can adopt various logics to determine paths, such as **A* pathfinding** or **flow field** algorithms. Despite their differences, both methods utilize similar parameters to calculate paths and provide directions for moving objects. The abstraction process involves selecting one of these methodologies, using abstracted parameters, and creating interfaces to enable the function to be called based on player-driven or AI-driven behaviors. 

### Researching

After clicking on a building's property, the player can select in the tab one of the available icons (and queue the next ones) if they got enough **resources**. On clicking icon the **research** will start loading. After finishing, it will activate its **skill** and leave the queue. The same goes to unit training but after finishing the unit will be summon.

Most of researching feature is specific and doesn’t need an abstracted part, because their information base is already created by entities part of researches feature. However, this feature is used to abstract player interaction with researches by describing **queues logic**, **research time calculation**, making necessary **checks** for researches availability, and possibly other business rules for interaction between player and researches.  

### Resource Gathering 

A team can earn resources by sending **units** to certain **structures**, this is called resource gathering. Its abstraction is pretty simple, and involves calculating the **resources earned**, which is equal to the **deposit draining**, and is based on **number of units**, and possibly **unit stats**. This feature also coordinates automaticaly the units’ behavior while gathering. It doesn’t need to have access to units movement, as well as any structure specifications, but it will have functions that can be called to coordinate the unit behavior in the context of its current activity being gathering, and at what phase it is on it. The phases of resource gathering are four: **moving to deposit**, **gathering**, **moving to base**, and **storing**, and they loop until the unit is assigned to another activity.

### Skills

Skills are special interactions cast by individual objects, which can be the player, heroes, units, and other entities, have a **limited range** for effects and are **punctual**. They have activation **cooldown**, and **cost**. Skills are often associated with combat and movements, changing stats of allies or enemies, causing damage, healing, accelerating conversion, etc.

This feature’s abstraction is very similar to abilities, focused on **rules**, which refer to how they are activated, including cooldown, activation cost, etc., and **effects**, which refer to what they do, including range, stats change, and others. Remember that abstraction covers only the generalization of skills, calculations, and logic for interaction interfaces, and not specific implementation details.   

## Game Modes

---

Game modes are different ways for the game to organize. It fundamentally involves **rules** and **goals**, and is the basis for how the other features interact with each other. Changing game modes involves changing the not only the specifications of each feature, but also when they are called, or how they are applied. If the game was a city, and features were houses or buildings, changing game modes would be akin the addresses, traffic rules, and even the roads itself. However, using the same analogy, the need to change the roads isn’t good for maintanability and scalability, so instead of changing the roads itself, it’s better to create roads that can accomodate well different types of rules and goals. 

This makes game modes to have very simple abstraction. They serve as presets for specific parts of the game to apply. For this reason, instead of explaining how the abstraction works for each game mode separately, there is a simple rule used by all of them: the abstraction involves defining the **rules**, which can be **strict** or **customizable**, what is **predefined** or **dynamic** (is generated by the game or player before or during the game), and what **features** will be used. Specific details and the way the features will be used, they will interact with each other is decided by other more specific components, which take the abstracted parts of each game mode and define every detail based on it. The individual explanation of game modes will be based solely on how user will see it.

### Campaign

Series of missions that will follow an story. It is the only game mode that involves a constant use of scenes. 

### Sandbox

A mode that allow the user to create custom maps, missions and other objects for the game.

### Skirmish

The player can use one of the maps or custom created in editor, set the sides by himself and game/victory conditions.

## Storage

---

Storage is a specific part of the game on itself, but it has an abstraction. Its abstraction involves how objects are organized in it. The understanding is very simple, but it’s important to create a well-structured dynamic for objects enter and exit on storage. Storage abstraction involves some rules for organizing it, which involves objects **classification**, **limits**, **quantities**, and other specifities. It’s important to note, however, that specific classes, limits, and quantities, as well as specifities, of course, aren’t part of the storage abstraction itself. The abstraction involves creating how classes work, how limits work, how quantities work. In other words, the abstraction ditates **rules** for the dataset.

The feature, for example, can have functions to organize storage in alphabetical order, or item value, or item quantity. These are functions that work as rules for organizing different objects on the dataset. It affects dataset **order**. The feature can also group objects based on the feature they are part of. Items can be grouped as unique “slots” objects in an inventory, while resources could be quantified without ocuppying slots in an inventory, without the need of storage at all. So, in simple words, the storage function defines what is stored, how inventories work, how objects are grouped, while not entering on specifities, and that’s why it’s said to only state the storage rules.
