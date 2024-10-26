using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using NUnit.Framework.Constraints;
using Phonebook;

namespace Task4.Tests
{
  internal class PhonebookTests
  {
    private Phonebook.Phonebook phonebook;
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      this.phonebook = new Phonebook.Phonebook();
    }

    [TearDown]
    public void TearDown()
    {
      this.phonebook.ClearSubscribers();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() 
    {
      this.phonebook = null;
    }

    [Test]
    public void AddSubscriber_NewSubscriber_AddedSuccesfully()
    {
      var subscriberId = Guid.NewGuid();
      var subscriberName = "Oleg";

      var expectedSubscriber = new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>());

      this.phonebook.AddSubscriber(expectedSubscriber);

      Assert.That(this.phonebook.GetSubscriber(subscriberId), Is.EqualTo(expectedSubscriber));
    }

    [Test]
    public void CreateSubscriber_NewSubscriberWithEmptyId_ThrowException()
    {
      var subscriberId = Guid.Empty;
      var subscriberName = "Oleg";

      Assert.Throws<ArgumentNullException>(() => new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>()));
    }

    [Test]
    public void AddNumberToSubscriber_NewSubscriberWithEmptyId_ThrowException()
    {
      var subscriberId = Guid.Empty;
      var subscriberName = "Egor";

      Assert.Throws<ArgumentNullException>(() => this.phonebook.AddNumberToSubscriber(new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>()), new PhoneNumber("1234", PhoneNumberType.Personal)));
    }

    [Test]
    public void AddNumberToSubscriber_NewEmptyNumber_ThrowException()
    {
      var subscriberId = Guid.NewGuid();
      var subscriberName = "Olegus";

      Assert.Throws<InvalidOperationException>(() => this.phonebook.AddNumberToSubscriber(new Subscriber(subscriberId, 
        subscriberName, new List<PhoneNumber>()), new PhoneNumber("", PhoneNumberType.Personal)));
    }

    [Test]
    public void RenameSubscriber_NewEmptyName_ThrowException()
    {
      var subscriberId = Guid.NewGuid();
      var subscriberName = "Olegus";

      Assert.Throws<InvalidOperationException>(() => this.phonebook.RenameSubscriber(new Subscriber(subscriberId, subscriberName, new List<PhoneNumber>()), ""));
    }

    [Test]
    public void UpdateSubscriber_NewEmptyGuidSubscriber_ThrowException()
    {
      var subscriberId = Guid.NewGuid();
      var subscriberName = "Masha";

      var subscriberEmptyId = Guid.Empty;
      var subscriberEmptyName = "Egor";

      Assert.Throws<ArgumentNullException>(() => this.phonebook.UpdateSubscriber(new Subscriber(subscriberId, subscriberName, 
        new List<PhoneNumber>()), new Subscriber(subscriberEmptyId, subscriberEmptyName, new List<PhoneNumber>())));
    }

    [Test]
    public void ValidatePhonenumber_NewPhonenumber_ThrowException()
    {
      var PhoneNumber = new PhoneNumber("1234",PhoneNumberType.Personal);

      Assert.Throws<ArgumentException>(() => PhoneNumberValidator.Validate(PhoneNumber));
    }

    [Test]
    public void CreatePhonenumber_NewEmptyPhonenumber_ThrowException()
    {
      Assert.Throws<ArgumentNullException>(() => new PhoneNumber("", PhoneNumberType.Personal));
    }
  }
}
