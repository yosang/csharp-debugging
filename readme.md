- [Debugging](#debugging)
- [Debugger](#debugger)
  - [Configuration](#configuration)
  - [Custom Configuration](#custom-configuration)
  - [build task](#build-task)
  - [Running the debugger](#running-the-debugger)
  - [Debugging practice](#debugging-practice)
- [Exception Handling](#exception-handling)
  - [Exception Classes](#exception-classes)
  - [Custom Exception Classes](#custom-exception-classes)
  - [Exception filters](#exception-filters)
- [Author](#author)

# Debugging
The art of finding, analyzing and correcting errors in code.

- `Syntax` errors - These are often detected by the compiler.
- `Logic` errors - Refer to code that is syntatically correct but does not behave as expected, these are hard to detect and can be found with a `debugger` tool.

# Debugger
A tool that allows code execution to pause at a breakpoint.

Once code is paused, we can analyze the current state of `variables`, `objects` etc...

## Configuration
Often, we dont really have to do a lot of configuration, the `IDE` will implement this for us.

To start the debugger we can press `Ctrl + Shift + D` then click on `Run and Debug`.
- This will run the debugger right away if already have a `launch.json` configured.
- If we havent used the debugger tool before on this project, chances are that we need to configure it.

To configure, after opening the debugger tab, click on `create a launch.json file`.
- From here choose whatever we are debugging it, for my case its `.NET 5 and .NET Core`.

It will open a new tab with the configuration details, here is what I got.

```json
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        
        {
            "name": ".NET Core Launch (console)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/bin/Debug/net9.0/debugging.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole",
            "stopAtEntry": false
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach"
        }
    ]
}
```

## Custom Configuration
if we dont get a configuration file automatically generated, we probably have to set it up ourselves.

We might get something like this
- From here, we add a double quotes `""` on `configurations` and should get some IntelliSense, 

```json
{
    "version": "0.2.0",
    "configurations": []
}
```

We can select multiple configurations
- We select `.NET Core Launch (console)`, and a template configuration should show up.

```json
{
    "version": "0.2.0",
    "configurations": [{
        "name": ".NET Core Launch (console)",
        "type": "coreclr",
        "request": "launch",
        "preLaunchTask": "build",
        "program": "${workspaceFolder}/bin/Debug/<target-framework>/<project-name.dll>",
        "args": [],
        "cwd": "${workspaceFolder}",
        "stopAtEntry": false,
        "console": "internalConsole"
    }]
}
```

We can see here that some stuff must be replaced:
- `<target-framework>` - This we can get from our `csproj` file under `<TargetFramework>`, for me its `net9.0`
- `<project-name.dll>` - This is usually just the name of the `csproj` file, but with a `.dll` extension instead, if in doubt, we can find it here `./obj/Debug/net9.0/debugging.dll`

The final version

```json
{
    "version": "0.2.0",
    "configurations": [{
        "name": ".NET Core Launch (console)",
        "type": "coreclr",
        "request": "launch",
        "preLaunchTask": "build",
        "program": "${workspaceFolder}/bin/Debug/net9.0/debugging.dll",
        "args": [],
        "cwd": "${workspaceFolder}",
        "stopAtEntry": false,
        "console": "internalConsole"
    }]
}
```

## build task
If for some reason the `IDE` complains that it cant find the `build` task, we must provide one.

Tasks for debugging are usually stored in a file `tasks.json` the same place `launch.json` lives.

If its not there, we simply create one, the skeleton looks something like this

Also, make sure that `"preLaunchTask": "build",` is present in the `launch.json` file.

The `label` of this task must match the one in `preLaunchTask`.

```json
{
    "version": "2.0.0",
    "tasks": []
}
```

Inside that `tasks` array, we add the following

```json
{
    "label": "build",
    "command": "dotnet",
    "type": "process",
    "args": [
        "build",
        "${workspaceFolder}/debugging.csproj",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary;ForceNoAlign"
    ],
    "problemMatcher": "$msCompile"
}
```

And we should be good to go.

## Running the debugger

Add a breakpoint somewhere in the code.

Go to the debugger tab with `Ctrl + Shift + D`.

Press `F5` to start the debugger.
- Press `F5` while debugging to continue.
- Press `Shift + F5` to stop the debugger.

Some other common usecases
- Press `F10` executes the current line, if its a function call, the debugger runs the function without entering its context.
- Press `F11` same as above, but enters the function call context and pauses at its first line.
- Press `Shift + F11` - to step out of a function call, the function completes its execution and we are paused back at the caller.

## Debugging practice
The best way to practice debugging is to see it in action.

Take the code provided in this example and follow these steps:
- Set a breakpoint inside `CountDown` on `message = $"Countdown: {i}";` right before a function call.
    - **Step into** with `F11`, you might have to press it twice to get to the `Broadcast` function call
        - Watch the debugger get inside the Broadcast execution context.
    - Once inside `Broadcast`, theres not much to debug here, only a console log
        - **Step out** with `Shift+F11`, which allows Broadcast to finish its execution and we are back at the caller.
            - We can verify this by seeing the message being printed in the Debug console, even though we didnt step over that line.
    - Once in `CountDown`, we can keep running the code by **Stepping Over** with `F10` as we know `Broadcast` is fine.
        - From here we can watch the variables `i`, `n` and `message` to see if our CountDown function behaves as expected.
    

Happy debugging.

# Exception Handling
Exceptions are types of errors that can arise during execution of code.

We can handle exception errors with:
- `Try` block - Includes code that might or might not result in an erroneous operation.
- `Catch` block - Takes the Exception type and a variable, in the code block we can handle what to do with the error object
    - We can log it, or re-throw it if we want.
- `Finally` block - This code block will run regardless of an error arising or not.

## Exception Classes
We have two classes that derive from the `System.Exception` class, which is the mother of all Exceptions.
- `System.ApplicationException` - Generated by the application program.
- System.SystemException - 

The System.SystemException has a bunch of classes we can work with, some of which are:
- `ArrayTypeMismatchException` - When an arrays type does not match.
- `DivideByZeroException` - When we try to divide by zero.
- `IndexOutOfRangeException` - When we try to access an index that is out of the array's index range.
- `InvalidOperationException` - When a method call is invalid for the current state.
- `NullReferenceException` - When we try to use a variable that is null as if it was an actual object.
- ...

## Custom Exception Classes
To create a custom exception, we simply inherit from `Exception`.

The constructor of this class can for simplicity use the `message` property, inherited from Exception with `base` keyword.

```c#
    // Custom Exception class
    public class NumTooLow : Exception
    {
        // Constructor inherits message from Exception base classwith base.
        public NumTooLow(string message) : base(message) { }
    }
```

To use this exception, we can use the `throw` keyword in our implementation logic:

```c#
public static int multiPlyByFive(int a)
{
    if (a < 1)
    {
        throw new NumTooLow("Number must be 1 or greater");
    }

    return a * 5;
}
```

Then we handle it in the `catch` block:

If we `throw` the exception in the catch block, and `Unhandled exception` will show up, followed by the Exception class and the stack trace of where the exception happened. This is useful so we can handle the exception.

However, we got to be careful we dont loose stack trace information when re-throwing exceptions:
- `throw <exception>` - Resets the stack trace
    - It treats the exception as a brand-new throw.
    - the stack trace now starts from the line where `throw err;` happened, instead of where it originally was thrown from.
    - we loose information about the entire root of the exception.
- `throw` - Keeps the stack trace, showing where the call happened, and where the exception was thrown from.
    - we keep all the information we need during debugging.

```c#
            // Using a custom exception
            try
            {
                // int result = multiPlyByFive(0); // Uncomment for an erroneous operation
                int result = multiPlyByFive(2); // Uncomment for a successful operation

                Console.WriteLine(result);
            }
            catch (NumTooLow err)
            {
                Console.WriteLine("Custom error: " + err.Message);
                throw;
            }
```

## Exception filters
Filters that allow us to check wether an Exception fits a specific criteria.

Instead of writing a bunch of catch blocks, we can simplify this by adding a condition.

Exception filters apply `when` the condition evaluates to true.

Most of the time we wont need this, but if we want to check for multiple conditions, like the `Exception` being a specific type, and the message containing something specific, then we can use it.

```c#
catch (Exception err) when (err is NumTooLow && err.Message.Contains("404"))

```

# Author
[Yosmel Chiang](https://github.com/yosang)