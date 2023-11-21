Readability is a crucial aspect of any project, whether it's an individual endeavor or a collaborative team effort. To maintain clarity and consistency in our codebase, we follow two key parameters: **Simple and Clean Architecture (SCA)** and **.NET C# coding conventions**.

In this section, we'll provide an overview of these guidelines to ensure that our project code remains organized and understandable. For more in-depth information, you can refer to the following links: [[SCA](https://genki-sano.medium.com/simple-clean-architecture-762b90e58d91) | [C#CC](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)]. This introductory guide will help you grasp the essentials for our specific project.

# Simple Clean Architecture (SCA)

---

First and foremost, Simple Clean Architecture (SCA) is designed with two primary objectives in mind:

1. **Consistency:** SCA establishes a set of rules that ensure code consistency, even when multiple individuals are collaborating on the project. This consistency is essential for maintaining code quality and readability.
2. **Decoupling:** SCA promotes a loosely coupled code structure, reducing interdependencies between various components. This decoupling allows for easier modifications and enhancements to different features within the codebase.

SCA is a code structure that builds upon concepts such as communication layers, abstraction, and the inversion of dependency. In essence, it provides a practical and straightforward implementation of Clean Architecture. Here are the key characteristics of SCA:

* **Layered Structure:** SCA is organized into distinct layers, with each layer having no direct access to the more external layers. This separation of concerns ensures that components within each layer remain encapsulated and isolated.
* **Mediator Layer:** A mediator layer acts as an intermediary, translating interactions between external layers and internal layers into 'events' and objects. This mediation enhances modularity and maintainability.
* **Unidirectional Information Flow:** In SCA, the flow of information follows a strict, unidirectional path. This design choice simplifies the understanding of how data moves through the architecture.

In the upcoming sections, we will delve deeper into the layers of SCA, explore the application of SOLID principles, and examine how information flows within this architecture. These insights will help us harness the full potential of SCA for our project. Stay tuned for a comprehensive overview.

## SCA Layers

In the realm of software development, SCA adheres to a fundamental principle: internal components are abstract, while external components are concrete. Why this distinction? The answer lies in the balance between flexibility and stability.

Concrete implementations, found in the external layers, are less flexible due to the dynamic nature of libraries, frameworks, and services that continually evolve. Meanwhile, internal layers embrace abstraction, offering a high degree of flexibility. This abstraction is not about compromising the rules or solidity of the software but rather focuses on abstracting the implementation details.

This concept provides a solid foundation for the SCA architecture, allowing us to build software that adapts to change while maintaining the core rules and principles. Let's now explore each SCA layer in more detail.

In fact, SCA is often seen as having four layers: entities, use cases, interactors, and services, but I will separate them in three for easier comprehension, and we can delve deeper on the four layers later.

### Core

The Core layer is the most abstract part of our application. It relies on a single concrete feature: its programming language, without dependence on language libraries. The Core has two interconnected components: business rules (entities) and application rules (use cases). In this part of the structure, all objects and classes are declared, and any manipulation between them occurs. However, it's crucial to note that the manipulation in this layer might require external components and processes. These components and processes are only requested from the exterior and are never used directly.

### Mediator

The Mediator layer is the most important aspect of the SCA, and it serves as the bridge between external devices, databases, services, and the Core. Its primary function is to translate data from these external sources into readable components and functions for the Core. This layer is the one most likely to undergo changes when external components evolve.

In this layer, any request made by the Core is received, and the Mediator uses the 'language' of external services to perform any necessary processes for transforming that 'language' into a format comprehensible to the Core. You might wonder whether this usage of the 'language' of external components violates the principle of maintaining a unidirectional information flow.

Indeed, you're correct that if the Mediator actively used the 'language' of external components for processing, it would disrupt the information flow. However, the Mediator's role is distinct: it only uses the 'language' of external components to receive information. It doesn't 'work' using the external components directly. Instead, its purpose is to receive information, translate it, and employ processes compatible with the external 'language' to perform this translation. The Mediator doesn't possess knowledge of the external components' specific details; it's purely focused on facilitating effective communication between the Core and the external world.

### Surface

The Surface layer can be likened to clothing; it plays a crucial role in ensuring the software functions as intended. However, it's not a core part of the software itself. Instead, it serves as a separate service. Just as Photoshop is a tool used to create images but isn't part of the image itself, the Surface layer in SCA is a tool for interacting with external services, processes, databases, and more.

It's important to note that the specific characteristics of the Surface layer, whether it's configured one way or another, don't impact the core business rules. This layer facilitates interactions that your software may not handle on its own. It allows an application to request services from multiple sources, and thanks to the Mediator layer, these requests can be seamlessly translated for the Core to utilize without necessitating changes to the Core layer.

## S.O.L.I.D

Before we delve into describing the layers in the usual project distribution, let's take a moment to understand the SOLID principles. It's important to note that SOLID serves as a set of code design principles rather than strict rules, providing guidance for structuring code effectively.

SOLID is an acronym representing a set of (code) design principles:

* **(S)ingle Responsibility Principle:** This principle emphasizes that each component of the code should have a single responsibility. In other words, each part of the code as a whole should be dedicated to solving a specific problem. This principle offers two primary benefits: ease of testability and loose coupling.
* **(O)pen-Closed Principle:** The core idea of this principle is to be "open for extension, closed for modification." This means that a functioning and tested class should remain open for extension but should not be modified. When a specific object requires a new behavior, a child class is created, inheriting from the original class and adding the needed specifications. The goal is to design classes with extensibility in mind, minimizing the need for changes, and eventually, closing them for modifications. The advantages of this principle are scalability and error prevention.
* **(L)iskov Substitution Principle:** This principle dictates that any child class should be capable of substituting its parent class without any behavioral changes affecting objects using the parent class. For example, if we have a ParentClass, ChildClass, object1 (of ParentClass), and object2 (of ChildClass), object1 should seamlessly transition to using ChildClass if, for some reason, ParentClass ceases to exist. If this transition isn't possible, it suggests that either ParentClass isn't sufficiently general, or ChildClass shouldn't inherit from it. It may be more appropriate for ChildClass to be a child of a more general class that encompasses both or to have no relationship with ParentClass at all. This principle benefits scalability, testing, and maintenance of the code.
* **(I)nterface Segregation Principle:** This principle extends the Single Responsibility Principle but operates at a higher level. It advocates the creation of smaller and more specific interfaces for software functions, features, or program components, rather than a single large interface with all functions. This not only empowers users to select which interfaces they want to use but also offers numerous benefits for code readability, loose coupling, maintenance, testing, scalability, and team task delegation.
* **(D)ependency Inversion Principle:** This principle is sometimes misunderstood due to the concept of dependency. Many believe that high-level modules should depend on low-level modules, or vice versa. In reality, the "inversion of dependency" refers to the concreteness of the module. Both high-level and low-level modules should depend on abstractions rather than details. The rule is simple: details come after abstractions. The more abstract a component, the more independent it should be and the more dependencies it has. Conversely, the more detailed a component, the more dependent it becomes, with fewer dependencies. This concept lies at the core of SCA, and its primary benefit is fostering loose coupling.

## SCA Layers on Highborne Battlecry

For our project, we utilize a comprehensive Simple Clean Architecture (SCA) consisting of four primary layers, complemented by an additional Interactor layer.

### Services

This layer encompasses the most external components of our project, those that exist outside of our core application, and are not intended for direct editing. Ideally, the codebase in this layer is encapsulated to such an extent that it remains unaware of the inner workings of the services it provides. In the context of Highborne Battlecry, this layer includes Unity and its associated plugins.

### External Interactors

Within the external Interactors layer, we find Gateways, Controllers, and Presenters. Each of these components serves distinct functions:

* **Gateways:** These are the bridges that receive input from the Services layer and translate it into the project's internal language. Subsequently, they transmit this transformed data to the next inner layer, depending on its relevance and context.
* **Controllers:** Controllers take on the responsibility of making decisions based on requests originating from inner layers as well as inputs from the Services layer. They are akin to the decision-makers within our system. For instance, they can receive a request for specific information, retrieve it from a database, or accept user input and determine whether to forward it to inner layers or the user interface.
* **Presenters:** Presenters operate as intermediaries for visual processes. Their primary role is to take requests from inner layers related to visual representation and facilitate their presentation in the user interface. Essentially, they construct a path for visual data to be displayed. This process is the reverse of what Gateways do, with the distinction that Presenters can only receive and provide outputs due to the specific information flow model of SCA.

This layer however, doesnt interact directly with the core. It sends information to the internal interactors layer, and receive requests from it.

### Internal Interactors

This layer acts as a pivotal bridge between the core features of the project, coordinating their interactions while ensuring loose coupling. It consists of the same components as the External Interactors layer but serves a distinct purpose. Here’s a deeper dive into its functionalities:

* **Gateways, Controllers, and Presenters:** Mirroring the External Interactors layer, these components handle translation, decision-making, and visual process intermediation. However, the crucial distinction lies in their interaction scope. Instead of directly engaging with the external services, this layer communicates with the Extermal Interactors layer and receives input from it, while receiving requests from the core.
* **Distinctive Coupling Structure:** Highborne Battlecry has an intriguing architecture. Its features are loosely interconnected while also maintaining loose coupling with external features. Imagine it as multiple independent cores, each representing a feature. However, within this framework, each feature views others as external services rather than integral parts of the core.
* **Coordination and Information Flow:** When a feature necessitates specific information, it submits a request to the Internal Interactors layer. Here, a controller decides whether to retrieve it from the External Interactors layer or another feature. How does this layer discern between internal and external data requirements? It possesses a comprehensive overview of all features. If a feature lacks certain information, it prompts the External Interactors layer for it.

### Use Cases

This layer serves as the epicenter for implementing business rules within each feature. It facilitates the execution of processes and rules specific to each feature, orchestrating their functionalities. Here’s a closer look:

* **Application of Business Rules:** The primary function of the Use Cases layer is to enact business rules within a feature. This involves executing processes and operations unique to that feature. For instance, if there’s a process like `UpdatePosition()` within the Movement feature, its function within the Use Cases layer would involve handling requests to update a unit's position. It retrieves position information from the Internal Interactors layer, performs updates, and communicates the updated data back to the respective controller for final execution in the Unit feature.

### Entities

This layer encapsulates all business rules within the project, encompassing not just variables but also processes and methodologies. Here’s a more comprehensive explanation:

* **Business Rules Repository:** The Entities layer is often misconstrued as a container solely for variables. In reality, it houses all business rules—be it classes, variables, methods, or other entities that define the operational and decision-making aspects of the project. For instance, consider the `UpdatePosition()` function discussed earlier; within the Entities layer, this function and its associated variables are declared. However, the operational logic and methods reside solely within the Use Cases layer.

## Conclusion

In essence, Simple Clean Architecture (SCA) is our project's backbone, meticulously designed to ensure clarity, maintainability, and flexibility. Its purpose is twofold: to establish consistency in code structure across collaborators and to foster a codebase that adapts effortlessly to evolving requirements.

Built upon the SOLID principles, SCA embodies a modular and adaptable approach to software development. Each layer operates with a specific function, promoting a clear separation of concerns while ensuring that components remain loosely coupled for easy modifications.

SCA functions by implementing distinct layers — from the Core to the Surface — with each layer having a designated role in handling interactions, processing data, and executing business rules. This architecture employs a unidirectional information flow, ensuring seamless communication between internal components while interfacing efficiently with external services.

Ultimately, SCA empowers our team to build and maintain a robust codebase that upholds core principles of software design. Its structured nature lays the foundation for scalable, maintainable, and comprehensible software systems.

# .NET C# Coding Convention

---

In contrast to the comprehensive structure of SCA, coding conventions offer a more stringent set of rules focused on consistency, testability, and readability. For the Highborne Battlecry project, we've adopted the .NET C# Coding Convention due to its widespread usage, adaptability, consistent updates, and efficient enforcement of coding standards. For a comprehensive understanding, you can refer to this link, where you'll find detailed guidelines. This section aims to provide a concise overview of the main elements, covering tools and analyzers, language guidelines, style conventions, and security practices.

| note: some parts of this guideline were directly extracted from .NET website.

## Tools and Analyzers

When creating a code, using `.editorconfig` is a way to enforce the rules to be followed. On Highborne Battlecry, it will be used the default style of .NET, which can be found on [dotnet/docs repo's file](https://github.com/dotnet/docs/blob/main/.editorconfig). This configuration seamlessly applies rules within compatible code editors. While Visual Studio is the recommended choice for compatibility, other similar programs can also be used. Additionally, the potential utilization of a code analyzer, specifically .NET Compiler Platform (Roslyn) analyzers, will be discussed for enhanced code analysis and adherence to standards.

## Language Guidelines

For coding samples, here are some generic rules extracted directly from .NET website:

* Utilize modern language features and C# versions whenever possible.
* Avoid obsolete or outdated language constructs.
* Only catch exceptions that can be properly handled; avoid catching generic exceptions.
* Use specific exception types to provide meaningful error messages.
* Use LINQ queries and methods for collection manipulation to improve code readability.
* Use asynchronous programming with async and await for I/O-bound operations.
* Be cautious of deadlocks and use [Task.ConfigureAwait](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.configureawait) when appropriate.
* Use the language keywords for data types instead of the runtime types. For example, use `string` instead of [System.String](https://learn.microsoft.com/en-us/dotnet/api/system.string), or int instead of [System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32).
* Use `int` rather than unsigned types. The use of `int` is common throughout C#, and it's easier to interact with other libraries when you use `int`. Exceptions are for documentation specific to unsigned data types.
* Use `var` only when a reader can infer the type from the expression. Readers view our samples on the docs platform. They don't have hover or tool tips that display the type of variables.
* Write code with clarity and simplicity in mind.
* Avoid overly complex and convoluted code logic.

Known the generic rules, let’s delve on specifications (you can look for some examples on .NET website):

### Strings Data

* For short strings concatenation, use strings interpolation.
* For large amounts of text, when using loops to append strings, use a [System.Text.StringBuilder](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder) object.

### Arrays

* Use concise syntax to initialize arrays (`arrayType[] arrayName = {’a’, ‘b,’, ‘c’};`). Don’t use var in this case.
* For explicit syntax, you can use var (`var arrayName = new arrayType[] {’d’, ‘e’, ‘f’};`)

### Delegates

* Use `Func<>` and `Action<>` for delegates instead of defining its type. Then define its mehod in a class.
* To call the function, use the signature defined by `Func<>`/`Action<>`.
* For declarating instances of a delegate, use the concise syntax. Using the delegate type, define a method that matches the signature within a class.
* For creating and calling an instance of delegate, use either full or condensed syntax.
* Condensed syntax (extracted from .NET): `Del exampleDel2 = DelMethod; exampleDel2("Hey");`.
* Full syntax: `Del exampleDel1 = new Del(DelMethod); exampleDel1("Hey");`.

### `try-catch` and `using` Statements in Exception Handling

* Use `try-catch` for most exception handling.
* If `try-finally` only has a call to Dispose method, use a `using` statement instead. Don’t use braces for it.

### `&&` and `||` Operators

* Use `&&` instead of `&`, and `||` instead of `|`.

### `new` Operator

* For object instanciation, use either `var firstExample = new ExampleClass();` or `ExampleClass instance2 = new();`.
* Use object initializers to simplify object creation.

### Event Handling

* Use a lambda expression to define an event handler that you don't need to remove later.

### Static Member

* Call static members by using the class name: *ClassName.StaticMember*. Don’t use derivated classes name for an object defined on base class.

### LINQ Queries

* Use meaningful names for query variables.
* For correct capitalization of property names of anonymous types, use aliases and Pascal case.
* Rename properties names in the result if ambiguous.
* Use implicit typing in the declaration of query variables and range variables (guide on next section topic).
* Align query clauses under `from` clause.
* Use `where` clause before other clauses (just after `from` clause).
* Use multiple `from` clauses instead of `join` to access inner collections.

### Implicitly Typed Local Variables

* Use implicit typing when variable type is obvious on right side of assignment (`var number = 10;`).
* If type isn’t clear, even if the name of the method is apparently descritive, don’t use `var` (`int number2 = Class.Result();`). A variable type is considered clear if it's a `new` operator, an explicit cast or assignment to a literal value.
* Use a variable type to specify a variable type. Use the variable name to specify its semantic information. Don’t mix these two.
* Use `dynamic` for run-time type inference of a variable. Don’t use `var` on its place.
* Use implicit type for loop variables in `for` loops.
* Don’t use implicit typing to determine a loop variable type in `foreach` loops.
* In LINQ queries, use implicit type for the result sequences.

Using `var` ensures compiler uses its *natural type* for an expression.

Place the `using` Directives Outside the `namespace` Declaration

`using` directives placed inside namespaces are context sensitive, which might be hard to maintain. Instead, use them outside, which makes the imported namespace a fully qualified name of the `using` directive. It avoids the possibility of `using` directives to be relative to a namespace instead of the fully qualified name of the namespace.

## Style Conventions

In general, use the following format for code samples:

* Use four spaces for indentation. Don't use tabs.
* Align code consistently to improve readability.
* Limit lines to 65 characters to enhance code readability on docs, especially on mobile screens.
* Break long statements into multiple lines to improve clarity.
* Use the "Allman" style for braces: open and closing brace its own new line. Braces line up with current indentation level.
* Line breaks should occur before binary operators, if necessary.

### Comment style

* Use single-line comments (`//`) for brief explanations.
* Avoid multi-line comments (`/* */`) for longer explanations. Comments aren't localized. Instead, longer explanations are in the companion article.
* For describing methods, classes, fields, and all public members use [XML comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/).
* Place the comment on a separate line, not at the end of a line of code.
* Begin comment text with an uppercase letter.
* End comment text with a period.
* Insert one space between the comment delimiter (`//`) and the comment text, as shown in the following example.

### Layout conventions

Good layout uses formatting to emphasize the structure of your code and to make the code easier to read. Microsoft examples and samples conform to the following conventions:

* Use the default Code Editor settings (smart indenting, four-character indents, tabs saved as spaces).
* Write only one statement per line.
* Write only one declaration per line.
* If continuation lines aren't indented automatically, indent them one tab stop (four spaces).
* Add at least one blank line between method definitions and property definitions.
* Use parentheses to make clauses in an expression apparent, as shown in the following code.

Exceptions are when the sample explains operator or expression precedence.

| note: This section was extracted from .NET website with almost no changes.

## Security

Security is essential in any software, even more on widely used ones. Highborne Battlecry isn’t different regards that. Despite cheating on campaign wouldn’t be a big deal, the game must ensure a fair and safe environment for players on multiplayer mode. To avoid problems like cheating, data leaking, virus injection, and toxicity, we will ellaborate on some measurements to be taken on different aspects.

### Resource Access

One of the characteristics of a secure code is its limited access to resources, like client/server data. This should be applied with even more caution when executing external code. Some techniques to ensure it are:

* Do not use Code Access Security (CAS).
* Do not use partial trusted code.
* Do not use the [AllowPartiallyTrustedCaller](https://learn.microsoft.com/en-us/dotnet/api/system.security.allowpartiallytrustedcallersattribute) attribute (APTCA).
* Do not use .NET Remoting.
* Do not use Distributed Component Object Model (DCOM).
* Do not use binary formatters.

When executing code of unknown origin, implement alternative security measures. Some of them are:

* Virtualization.
* AppContainers.
* Operating system (OS) users and permissions.
* Hyper-V containers.

### Security-Neutral Code

This kind of code doesn’t use explicitly any security system. It might fail to catch security exceptions, but it will still benefit from security technologies described on .NET website. Using simple words, security-neutral code is a code that is loosely coupled from any security system. Remember, though, that security-neutral libraries, codes, and applications need to have permissions allowed whenever it’s called. For example, we could call a code, which needs given permission, and the code calls an application. That application will also need the permission to work properly. If any of these parts don’t have the needed permissions, a security exception will happen.

### Not Reusable Application Code

Some code components aren’t used for other applications, which makes their security simple. However, malicious code is still able to call that code, regardless it not being intended to do so, being able to read field values, and surface data, despite not being able to access resources due to code access security. This is important, because some field values can store sensitive information temporarily. Have this in mind when writing the code, specially when it comes to user-inputable information.

### Managed Wrapper to Native Code Implementation

When writing managed wrappers, don’t give unmanaged rights to all applications that use it through platform invoking, COM interop, or any other means. Instead, give rights only to the wrapper code.

### Library Code that Exposes Protected Resources

This process should be taken with a lot of caution. Use libraries as interfaces for other codes to resources that wouldn’t be available otherwise. However, reinforce the use of permissions for the resources the code uses. Wherever you expose a resource, your code must first demand the permission appropriate to the resource (that is, it must perform a security check) and then typically assert its rights to perform the actual operation.
