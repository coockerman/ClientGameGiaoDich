using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordReset
{
    public string username;
    public string passwordOld;
    public string passwordNew;

    public PasswordReset() {}
    
    public PasswordReset(string username, string passwordOld, string passwordNew) {
        this.username = username;
        this.passwordOld = passwordOld;
        this.passwordNew = passwordNew;
    }
}
