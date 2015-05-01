using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

// этот класс представляет сообой всех людей
class Person
{
	// словарь в котором хранятся все люди
	static public Dictionary<string, Person> person_dictionary = new Dictionary<string, Person>();

	// имя человека
	public string givn;
	// фамилия
	public string surn;
	// true - мужик, false - девушка
	public bool sex_male;

	// люди появляются на самом деле вот так
	public Person(string givn, string surn, bool sex_male)
	{
		this.givn = givn;
		this.surn = surn;
		this.sex_male = sex_male;
	}
}

// этот класс представляет сообой все семьи
class Family
{
	// словарь в котором хранятся все семьи
	static public Dictionary<string, Family> family_dictionary = new Dictionary<string, Family>();

	// муж
	public Person husband;
	// жена
	public Person wife;
	// словарь в котором мы храним детей
	public Dictionary<string, Person> children = new Dictionary<string, Person>();

	// так зарождаются семьи
	public Family(Person husband, Person wife, Dictionary<string, Person> children)
	{
		this.husband = husband;
		this.wife = wife;
		this.children = children;
	}
}