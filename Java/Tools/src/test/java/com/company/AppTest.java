package com.company;

import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

public class AppTest extends TestCase
{
    /**
     * Create the test case
     *
     * @param testName name of the test case
     */
    public AppTest( String testName )
    {
        super( testName );
    }

    /**
     * @return the suite of tests being tested
     */
    public static Test suite()
    {
        return new TestSuite( AppTest.class );
    }

    /**
     * Rigourous Test :-)
     */
    public void testencrypt(){
        String s=AESUtil.encrypt("8435u90503250","szoaznzwbgxtdbjk");
        System.out.println(s);
        assertEquals("b2feB27R6NDcavxvIvtwEQ==",s);

    }

    public void testdecrypt(){
        String s=AESUtil.decrypt("b2feB27R6NDcavxvIvtwEQ==","szoaznzwbgxtdbjk");
        System.out.println(s);
        assertEquals("8435u90503250",s);

    }
}
