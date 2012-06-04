cloaked-hipster
===============

### The Rant

A discussion yesterday about XAML versus HTML degenerated into much debate around how terrible the XAML syntax is for declaring resources.

    <Style TargetType="TextBlock" x:Key="TitleText">
      <Setter Property="Background" Value="Blue"/>
      <Setter Property="DockPanel.Dock" Value="Top"/>
      <Setter Property="FontSize" Value="18"/>
      <Setter Property="Foreground" Value="#4E87D4"/>
      <Setter Property="FontFamily" Value="Trebuchet MS"/>
      <Setter Property="Margin" Value="0,40,10,10"/>
    </Style>

So very very verbose.

Contrast that with the equivalent CSS:

    titletext 
    { 
       background: blue;
       font-size: 18;
       foreground: #4E87D4;
       font-family: Trebuchet MS;
       margin: 40px 10px 10px 0; // note how the order is different
    }

### The Wager

So one wise-guy, who shall remain nameless, said "Why can't you just use CSS?" and after more debate I ran out of things that were dealbreakers.

So this little sandbox is an experiment - using Twitter Bootstrap as a reference stylesheet - to allow you to use CSS in your XAML apps.

### The implementation

I think the best approach for this is to use T4 templates, so the code might look like 

    <#@ template language=“C#” #>
    <#@ output extension=".xaml" #>
    <#@ assembly name="$(SolutionDir)\tools\CSSAML.dll" #>
    
    <ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    
    <#= CSSAML.Generate(CssContents) #>

    </ResourceDictionary>

    <#+
    string CssContents = “titletext { background: blue; font-size: 18; foreground: #4E87D4; font-family: Trebuchet MS;   margin: 40px 10px 10px 0; }”;
    #>

And how would you use this?

 1. Open a XAML app
 2. File -> New Item -> Select "New CSS Resource" file -> Add
 3. Paste in CSS contents into value. Save file.
 4. Build process runs T4 engine and spits out a XAML resource file
 5. Reference new resource dictionary in your App.xaml file


### Notes:

First off, we need a way to map CSS classes and the actual styles to XAML style elements - there's likely to be some things which are not applicable on each side - so how do we deal with those?

Secondly, XAML has its own idioms for doing inheritance and determining what controls are associated with each style. We need to have some conventions to infer (and override?) these patterns from arbitrary CSS files.

### Are you mad?

Yes.