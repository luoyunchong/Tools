package com.company;

import java.io.Serializable;
import java.net.URLDecoder;
import java.net.URLEncoder;

public class App {

    public static void main(String[] args) throws Exception {
        String password="12345678";
        String sourceString="{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
        String den = DESUtil.encrypt(sourceString,password);
        System.out.println("加密:"+den);

        String CHARSET="utf-8";
        String encodeStr= URLEncoder.encode(den,CHARSET);
        System.out.println("url编码:"+den);

        String decodeUrl=URLDecoder.decode(encodeStr,CHARSET);
        System.out.println("url解码："+decodeUrl);

        String ert = DESUtil.decrypt(decodeUrl,password);
        System.out.println("url解密："+ert);

/*
*
加密:3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
url编码:3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
url解码：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
url解密：{'idcard':'330781198509077211','date':'2021-11-11 19:04'}
*
* */

        //MD5

        String x= MD5Util.getMD5Base64(sourceString);
        System.out.println("md5："+x);

    }

    //不会java反序列化。
    class User implements Serializable {
        private  String idcard;
        private  String date;

        public String getIdcard() {
            return idcard;
        }

        public void setIdcard(String idcard) {
            this.idcard = idcard;
        }

        public String getDate() {
            return date;
        }

        public void setDate(String date) {
            this.date = date;
        }

    }

}
