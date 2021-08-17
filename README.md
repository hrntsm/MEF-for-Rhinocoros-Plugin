# MEF for Rhinoceros Plugin

## What is MEF?

- [MEF (Managed Extensibility Framework) - Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/framework/mef/)
- [Managed Extensibility Framework入門 まとめ](https://blog.okazuki.jp/entry/20110507/1304772329)

## About this repository

I saw [New way to load a C# .net plugin](https://discourse.mcneel.com/t/new-way-to-load-a-c-net-plugin/31284) on Rhinoceros Forums and it looked interesting, so I'm trying it out. I'm trying it out.

I'm doing this by following the instructions below.
However, since the target was written in VisualStudio2010 for Rhino4, I have rewritten it to be compatible with the current Rhino7.9 and VisualStudio2019.

https://www.codeproject.com/Articles/1091178/Managed-Extensibility-Framework-MEF-Plugin-for-Rhi

The original article was published under [The Code Project Open License (CPOL) 1.02](https://www.codeproject.com/info/cpol10.aspx).

## How to use

For more detail, please see the original article.

1. Build the solution
1. Drag and drop RhinoPluginLoader.rhp in RhinoPluginLoader/bin/ into Rhino so that the command "LoadPlugin" can be used.
1. When you execute the LoadPlugin command, you will be asked for the dll path, so refer to the dll file where the command you want to execute is actually written.
    - As an example, the csproj to create a Line is RhinoPluginMakeLine, so refer to the dll file in its bin
1. The Rhino commands described in MakeLine.cs will be executed.
1. Normally, Rhino needs to be restarted when the command is rewritten, but in this case, MEF is used to load dll each time the LoadPlugin command is executed, so there is no need to restart.
1. ;DDDDDDDDDDDDDDD
