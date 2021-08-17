# MEF for Rhinoceros Plugin

## MEF とは

- [MEF (Managed Extensibility Framework) - Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/framework/mef/)
- [Managed Extensibility Framework入門 まとめ](https://blog.okazuki.jp/entry/20110507/1304772329)

## このリポジトリの内容について

Rhinoceros Forums で [New way to load a C# .net plugin](https://discourse.mcneel.com/t/new-way-to-load-a-c-net-plugin/31284) をみて面白そうだったので試しています。

以下をなぞってやっています。ただし、ターゲットを Rhino4 向けで VisualStudio2010 で書かれたもののなので、現状の Rhino7.9, VisualStudio2019 に対応した形に書き直してます。

https://www.codeproject.com/Articles/1091178/Managed-Extensibility-Framework-MEF-Plugin-for-Rhi

元の記事は [The Code Project Open License (CPOL) 1.02](https://www.codeproject.com/info/cpol10.aspx) で公開されています。

## 使い方

詳細は上記記事を見てください。

1. ソリューションをビルドする
1. RhinoPluginLoader/bin/ の中にある RhinoPluginLoader.rhp を Rhino にドラッグアンドドロップして、コマンド "LoadPlugin" を使えるようにする。
1. LoadPlugin コマンドを実行すると dll のパスを求められるので、実際に実行したいコマンドが書かれた dll ファイルを参照する。
    - 例として Line を作成する csproj が RhinoPluginMakeLine があるので、その bin にある dll ファイルを参照する
1. MakeLine.cs 内でかかれた Rhino コマンドが実行される。
1. 通常コマンドを書き換えた場合 Rhino の再起動が必要だが、ここでは MEF を使って LoadPlugin コマンド実行ごとに dll を読み込んでいるので再起動の必要がない
1. ٩(๑＞◡＜๑)۶
