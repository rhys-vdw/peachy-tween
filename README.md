# PeachyTween :peach:

[![standard-readme compliant](https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

A tweening for Unity designed to be simple to use and extend.

## Background

Created as an alternative to [DOTween](https://github.com/Demigiant/dotween/), which has a proprietary license with some limitations. PeachyTween intends to create a similarly powerful tweening library that can be installed by UPM and allows public forks and distribution.

PeachyTween does not aim to be API compatible with DOTween, not to fully mirror its functionality.

:warning: **PeachyTween is still in early development, and is untested**

## Install

_Not yet available to install._

## Usage

```cs
using UnityEngine;
using PeachyTween;

public class PeachyExample
{
  void Start()
  {
    Peachy.Tween(
      from: 0,
      to: 5,
      onChange: v => Debug.Log($"value = {v}"),
      duration: 4f
    );

    transform
      .TweenLocalRotation(Quaternion.Euler(90, 90, 90), 2f)
      .Slerp()
      .Ease(Ease.BounceInOut)
      .Loop(4)
      .OnLoop(() => Debug.Log("Loop complete"))
      .OnKill(() => Debug.Log("Four loops complete"));
  }
}
```

## Contributing

_Contributions will be welcome after release._

Follow progress in the [GitHub development project](projects/1).

## License

[MIT © Rhys van der Waerden](LICENSE)