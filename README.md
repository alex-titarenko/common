# TAlex.Common
[![NuGet Version](http://img.shields.io/nuget/v/TAlex.Common.svg?style=flat)](https://www.nuget.org/packages/TAlex.Common/) [![NuGet Downloads](http://img.shields.io/nuget/dt/TAlex.Common.svg?style=flat)](https://www.nuget.org/packages/TAlex.Common/)

 Base class library for commonly occurring problems.

## Features
#### Extensions:
* **Assembly**: get title, description, product, copyright, etc. of assembly.
* **Expression**: get property name from expression.
* **String**: split string by newline delimeter.

#### Services:
* **Undo**: provides ability to create undo/redo functionality in your apps.

## Example of usage
In many cases helpfull be able to get property name without writting a string. For example you develop WPF application and you want to create DependencyProperty and for registering this property you must put property name as a string, on another common situation when you create view model and use ```INotifyPropertyChanged``` approach, to avoid painful situation when you will have a bug after inattentive renaming you can just use this extension:

```C#
private class SimpleModel
{
    public string Text { get; set; }

    public void TestMethod()
    {
        var textPropertyName = PropertyName.Get(() => Text);
        Console.WriteLine(textPropertyName); // output will be Text
    }
}
```

## Get it on NuGet!

    Install-Package TAlex.Common

## License
TAlex.Common is under the [MIT license](https://github.com/T-Alex/Common/blob/master/LICENSE.md).
