using System.Collections;
using System.Collections.Generic;
using EasyMobile;
using EasyMobile.Demo;
using UnityEngine;
using UnityEngine.UI;

public class DemoUtils_Contacts : MonoBehaviour {
    [SerializeField]
    private uint avatarWidth = 256, avatarHeight = 256;

    [SerializeField]
    private Color[] avatarColors = new Color[] { Color.black, Color.white };

    [SerializeField]
    private RawImage avatarImage = null;

    [SerializeField]
    private ContactView contactViewPrefab = null;

    [SerializeField]
    private Transform contactViewRoot = null;

    private List<ContactView> createdViews = new List<ContactView>();

    public void GenerateAvatar()
    {
        avatarImage.texture = TextureGenerator.GenerateRandomTexture2D((int)avatarWidth, (int)avatarHeight, avatarColors);
    }

    public void ClearAvatar()
    {
        avatarImage.texture = null;
    }

    public void AddContactView(Contact contact)
    {
        var contactView = Instantiate(contactViewPrefab, contactViewRoot);
        contactView.UpdateContact(contact);
        contactView.gameObject.SetActive(true);
        createdViews.Add(contactView);
    }

    public void ClearContactViews()
    {
        if (createdViews == null || createdViews.Count < 1)
            return;

        foreach (var view in createdViews)
            Destroy(view.gameObject);

        createdViews.Clear();
    }

    public void DeleteContactAfter(ContactView view)
    {
        createdViews.Remove(view);
        Destroy(view.gameObject);
    }


}
