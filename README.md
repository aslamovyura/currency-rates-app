# Currency Rates App

![.NET Core](https://github.com/aslamovyura/currency-rates-app/workflows/.NET%20Core/badge.svg)

"Currency Rates" is a Console / CLI application for checking currency exchange rates using an open API (http://www.nbrb.by).

## Getting Started (CLI)

Run the following command from application Release directory and follow the instructions:

```
> .\AppCLI --help
```

Check exchange rate of a specific currency (e.g. USD):

```
> .\AppCLI USD
```

Get the whole list of currency exchange rates:

```
> .\AppCLI --all
```

Show currency exchange rate (e.g. EUR) and save results to a txt file:

```
> .\AppCLI EUR --save
```

## Authors

[Yury Aslamov](https://aslamovyura.github.io/),
[Alexander](),
[Polyna]().

## License

This project is under the MIT License - see the [LICENSE.md](https://github.com/aslamovyura/currency-rates-app/blob/master/LICENSE) file for details.