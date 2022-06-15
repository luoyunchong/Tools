package com.company;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Base64;

public class Md5Util {
    public static String getYNMD5Base64( String srcString) {
        MessageDigest md = null;
        try
        {
            md = MessageDigest.getInstance("MD5");
        }
        catch( NoSuchAlgorithmException ex)
        {
            ex.printStackTrace();
            return null;
        }
        md.update( srcString.getBytes() );
        String encoded = Base64.getEncoder().encodeToString(md.digest());
        return encoded.toUpperCase();
    }
}
