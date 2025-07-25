// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.Domain;


[TestFixture]
public class LocalizableTests
{
    [Test]
    public void NewFromDefault()
    {
        var sut = default(Localizable);

        Check.That(sut.ResourceKey).IsNull();
        Check.That(sut.Args).IsNull();
        Check.That(sut.ToString()).IsEqualTo("[]");
    }

    [Test]
    public void NewFromDefaultConstructor()
    {
        var sut = new Localizable();

        Check.That(sut.ResourceKey).IsEmpty();
        Check.That(sut.Args).IsEmpty();
        Check.That(sut.ToString()).IsEqualTo("[]");
    }

    [Test]
    public void NewFromConstructorString()
    {
        var sut = new Localizable("key");

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).IsEmpty();
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromConstructorStringArguments()
    {
        var sut = new Localizable("key", 123, "bbb");

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123, "bbb");
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }


    [Test]
    public void NewFromStringImplicitCast()
    {
        Localizable sut = "key";

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).IsEmpty();
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromTupleImplicitCastStringArgs1()
    {
        Localizable sut = ("key", 123);

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123);
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromTupleImplicitCastStringArgs2()
    {
        Localizable sut = ("key", 123, "bbb");

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123, "bbb");
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromTupleImplicitCastStringArgs3()
    {
        Localizable sut = ("key", 123, "bbb", 12.3);

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123, "bbb", 12.3);
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromTupleImplicitCastStringArgs4()
    {
        Localizable sut = ("key", 123, "bbb", 12.3, "c");

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123, "bbb", 12.3, "c");
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromTupleImplicitCastStringArgs5()
    {
        Localizable sut = ("key", 123, "bbb", 12.3, "c", 999);

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123, "bbb", 12.3, "c", 999);
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }

    [Test]
    public void NewFromTupleImplicitCastStringArgs()
    {
        Localizable sut = ("key", [
            123,
            "bbb",
            12.3,
            "c",
            999,
        ]);

        Check.That(sut.ResourceKey).IsEqualTo("key");
        Check.That(sut.Args).ContainsExactly(123, "bbb", 12.3, "c", 999);
        Check.That(sut.ToString()).IsEqualTo("[key]");
    }
}
