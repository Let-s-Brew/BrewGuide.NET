# BrewGuide.NET
A simple .NET NuGet library for Homebrew Style Guidelines.

## Usage
Currently only the 2021 BJCP Style Guidelines are built in, and are accessible via:

```BrewCode.BrewGuide.BJCP.BJCPGuidelines.BJCP2021Guidelines```

Which is a `BrewCode.BrewGuide.BJCP.BJCPGuideLines` object, implementing the `BrewCode.BrewGuide.IGuidelines` Interface.

You may utilize the `IGuidelines`, `IStyleCategory`, and/or `IStyle` interfaces within your own project to reference any style guidelines. 