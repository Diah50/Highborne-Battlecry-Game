# Repository Map
---

While our repository structure aims to be intuitive and easy to follow, you might encounter uncertainties or find certain specifications for folder and file creation a bit vague. To address these potential concerns, we've created a repository map. This document is designed to provide a better understanding of each folder and its contents.

## Repository Specifics

Repository specifics encompass files and folders that aren’t directly part of the project itself but rather contain specifications and settings for organizing and structuring the repository. These elements facilitate proper organization and management on platforms like GitHub.

### .gitignore

This file holds significant importance for the project as it ensures that certain files—such as settings files or sensitive data—aren’t included during repository cloning. It's crucial to maintain this file with the correct entries, ensuring that only the necessary files are included in the repository.

### .vsconfig

The .vsconfig file shows what configurations are being used by the repository.

### README.md (Repository)

The README.md file serves as a vital information hub about the project, intended for anyone—from developers to potential users. It may contain installation guides, project summaries, or crucial details the creator wants others to know. However, it's advisable to maintain a concise format, avoiding lengthy content.

### .github Directory

This directory houses all information pertinent to GitHub processes, separate from the core project. It serves as a repository for elements such as pull request templates, standardized issue templates, or commit standards. Anything related to GitHub-specific workflows and standards can be found here.

### .vscode Directory

The .vscode directory works in tandem with the .vsconfig file, housing tasks related to extension installations, launch configurations, setting tasks, and editor configurations. It ensures that anyone cloning the repository has all the necessary files to work seamlessly within the project environment. This directory is instrumental in enforcing and maintaining project standards, minimizing inconsistencies, confusion, and human error. It contains specific settings for VS Code tailored to this project's requirements.

## UI

The UI directory is intended to store all the files, and directories related to user interface, from front end to backend. It houses UI features and any external services used within it. The project follows Simple Clean Architecture (SCA), so it’s important to create files for directories that follow the project structure. It also has all external interactors files, according to [Highborne Battlecry structure](https://coda.io/d/_dXpIsBsS8SH/_su4mF), in its root.

### Features

Feature directories encompass any aspect considered a game feature based on business rules, such as the Main Menu or in-game interface. They have at least these following files: *FeatureEntities*, *FeatureUseCases*, *FeatureInterface* (internal interactors), changing *Feature* by the actual directory. 

### UI Elements

This directory relates to the building blocks of UI. In other words, it’s the UI structure. It works directly with UI Toolkit, but through abstractions. However, it's not listed under features as it doesn't involve business rules. It is an application of business rules stated on features, and a step before visual presentation through toolkit. It works through abstraction, but is in fact a specification of UI. 

### UI Toolkit

This directory is intended to store the UI Toolkit files, which are an external service.

## Docs

This directory is simple to understand, but very important. It houses all documentation for the project via .md files (note, for the project, and not for scripts. Script documentation should be created using .XML files within the corresponding directory). It should receive constant updates, and is partially cloned on [coda.io](https://coda.io/d/_dXpIsBsS8SH/_sukCk). It contains the following files:

* **CODE_OF_CONDUCT.md:** Is the code of conduct for any player.
* **CONTRIBUTING.md:** Contains the names of people that contributed to the project.
* **LICENSE.md:** Is the permission to use the game, and the conditions for it.
* **README.md:** Contains essential information about the Docs directory.
* **CodeGuidelines.md:** Has all the standards, rules and principles to work on the project. 
* **DevelopmentRoadmap.md:** Documents the project’s versioning.
* **ProjectBlueprint.md:** Is a guide for the project developping with technical information.
* **RepositoryMap.md:** Has information of how the repository is structured, how to navigate through it, and organize it.

## Settings

Settings directory is similar to features, however, it works directly with the game engine and other tools. Because of that, it’s a more specific and low-level component than in-game features.. Because of that, it has a separated directory. Settings specifications are still to be discussed, but any changeable or not setting files for game video, audio, or other specifications, should be placed here.

## External Assets

This directory is very simple. Any services that are not part of the project should be placed here. Databases, packages, and plugins are some of the services that might be stored in this directory. 

## Internal Assets

Internal Assets comprehend most of the codebase content. Here are stored all audio, video, scripts, and anything that is part of the software.

### Temp

Houses any temporary archives, like cache, data chunks, file managing data, and any other temporary files. The files stored here use the suffix .tmp. It’s important to destinate tempory files for this save during the program execution, and always clean it before the program ends. Some temporary files might persist after program end, making it easier for the program to be used again without the need to load all files again. However, this is something that should be used with a lot of caution, and avoided if possible.

### Resources

This directory is intended to store images, video, audio, or any other multimedia resources. It’s separated in:

* Audio;
* Sprites;
* Scenes;
* Presskit.

Since different situations might require different file formats, this section will not cover them. Remember, though, to use optimal file formats, and to document it. It’s important to also use descriptive names, and despite the files being separated in different directories, having breadcrumbs on the name can be helpful. If the disposition of folders change, for example, locating files through breadcrumbs on the file name itself can avoid the need to make substantial changes on code that call it. For example, it’s possible to name a tileset as TilesetGrassBottom, for example, so when script calls bottom part of grass tileset, it will call Tileset component on name, then Grass, then Bottom. This way, it is possible to maintain a loosely coupled structure, since a class can call tileset, and other derivated classes can call other parts of the name, and that could be changed easily at any time.

### Scripts

Scripts are very important to the project, and possibly the most complex part of it, that’s why organization is very important. The main programming language used on the project is c#. The scripts directory is divided on sections, and each section has its own features. On the root of scripts directory, there will be all the external interactors/interfaces. For each feature, there will be its proper internal interactors/interfaces, use cases, and entities. Any script explanation should be documented through .xml files.
