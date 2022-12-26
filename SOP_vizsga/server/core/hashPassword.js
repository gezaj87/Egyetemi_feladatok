import crypto from 'crypto';

class HashPassword
{
    static Encrypt(secret, text) {
        
        const iv = crypto.randomBytes(16);
        

        var mykey = crypto.createCipher('aes-128-cbc', secret);
        var mystr = mykey.update(text, 'utf8', 'hex')
        mystr += mykey.final('hex');
        return mystr;
        
    }

    static Decrypt(secret, token)
    {
        var mykey = crypto.createDecipher('aes-128-cbc', secret);
        var mystr = mykey.update(token, 'hex', 'utf8')
        mystr += mykey.final('utf8');
        return mystr;
    }


}

export default HashPassword;