using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliographyParser.Tests
{
    [TestClass]
    public class InMemoryItemRepoTests
    {
        private Item _item1;
        private Item _item2;
        private Item _item3;

        [TestInitialize]
        public void BeforeTests()
        {
            _item1 = new Item(Item.ItemType.Book, new Dictionary<Item.FieldType, string> {[Item.FieldType.Author] = "Jacob"});
            _item2 = new Item(Item.ItemType.Book, new Dictionary<Item.FieldType, string> {[Item.FieldType.Author] = "Steven"});
            _item3 = new Item(Item.ItemType.Book, new Dictionary<Item.FieldType, string> {[Item.FieldType.Author] = "Paolo"});
        }

        [TestMethod]
        public void GetAllTest_Repo_Is_Empty()
        {
            var repo = new InMemoryItemRepo();
            Assert.AreEqual(0, repo.GetAll().Count());
        }

        [TestMethod]
        public void Add()
        {
            var repo = new InMemoryItemRepo();
            repo.Add(_item1);
            Assert.AreEqual(1, repo.GetAll().Count());
            Assert.AreSame(_item1, repo.GetAll().First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_Argument_Null()
        {
            var repo = new InMemoryItemRepo();
            repo.Add(null);

        }

        [TestMethod]
        public void Add_Dublicates()
        {
            var repo = new InMemoryItemRepo();
            repo.Add(_item1);
            Assert.AreEqual(1, repo.GetAll().Count());
            Assert.AreSame(_item1, repo.GetAll().First());

            repo.Add(_item1);
            Assert.AreEqual(1, repo.GetAll().Count());
            Assert.AreSame(_item1, repo.GetAll().First());
        }

        [TestMethod]
        public void AddRange()
        {
            var repo = new InMemoryItemRepo();

            repo.AddRange(new List<Item> {_item1, _item2, _item3});

            Assert.AreEqual(3, repo.GetAll().Count());
            Assert.AreSame(_item1, repo.GetAll().ToList()[0]);
            Assert.AreSame(_item2, repo.GetAll().ToList()[1]);
            Assert.AreSame(_item3, repo.GetAll().ToList()[2]);
        }

        [TestMethod]
        public void AddRange_EmptyList()
        {
            var repo = new InMemoryItemRepo();

            repo.AddRange(new List<Item>());

            Assert.AreEqual(0, repo.GetAll().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRange_Null_Argument()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(null);
        }

        [TestMethod]
        public void Search_SingleResult()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> {_item1, _item2, _item3});

            var r = repo.Search("Jacob").ToList();
            Assert.AreEqual(1, r.Count);
            Assert.AreSame(_item1, r.First());
        }

        [TestMethod]
        public void Search_SingleResult_CaseInsensitivity()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("jacob").ToList();
            Assert.AreEqual(1, r.Count);
            Assert.AreSame(_item1, r.First());
        }

        [TestMethod]
        public void Search_MultipleResults()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("a").ToList();
            Assert.AreEqual(2, r.Count);
            Assert.AreSame(_item1, r[0]);
            Assert.AreSame(_item3, r[1]);
        }

        [TestMethod]
        public void Search_SingleResult_Take_Zero()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("Jacob",0).ToList();
            Assert.AreEqual(0, r.Count);
        }

        [TestMethod]
        public void Search_SingleResult_Take_One()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("Jacob", 1).ToList();
            Assert.AreEqual(1, r.Count);
            Assert.AreSame(_item1, r.First());
        }

        [TestMethod]
        public void Search_MultipleResults_Take_Zero()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("a", 0).ToList();
            Assert.AreEqual(0, r.Count);
        }

        [TestMethod]
        public void Search_MultipleResults_Take_One()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("a",1).ToList();
            Assert.AreEqual(1, r.Count);
            Assert.AreSame(_item1, r[0]);
        }

        [TestMethod]
        public void Search_MultipleResults_Take_Two()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });

            var r = repo.Search("a",2).ToList();
            Assert.AreEqual(2, r.Count);
            Assert.AreSame(_item1, r[0]);
            Assert.AreSame(_item3, r[1]);
        }

        [TestMethod]
        public void Search_EmptyString()
        {
            var repo = new InMemoryItemRepo();
            repo.AddRange(new List<Item> { _item1, _item2, _item3 });
            var r = repo.Search("").ToList();
            Assert.AreEqual(0, r.Count);
        }
    }
}