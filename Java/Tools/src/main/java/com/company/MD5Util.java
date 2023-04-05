package com.company;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Base64;

public class MD5Util {
    public static String getMD5Base64( String srcString) {
        MessageDigest md = null;
        try
        {
            md = MessageDigest.getInstance("MD5");
        }
        catch(NoSuchAlgorithmException ex)
        {
            ex.printStackTrace();
            return null;
        }
        md.update( srcString.getBytes() );
        String encoded = Base64.getEncoder().encodeToString(md.digest());
        return encoded.toUpperCase();
    }
}
