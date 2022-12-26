class Helper
{
    static ErrorMessageShow(elements = true, show = false, success = false)
    {
        if (elements == true && show == false)
        {
            const element1 = document.getElementById('registerError');
            const element2 = document.getElementById('registerSuccess');

            element1.className = "alert alert-danger mt-3 text-center d-none";
            element2.className = "alert alert-danger mt-3 text-center d-none";
            return;
        }

        const element = document.getElementById(elements);
        if (success) element.className = "alert alert-success mt-3 text-center";
        else element.className = "alert alert-danger mt-3 text-center";
        
    }

    static checkDigit(event) {
        const code = (event.which) ? event.which : event.keyCode;
    
        if ((code < 48 || code > 57) && (code > 31)) {
            return false;
        }
    
        return true;
    }

    static cc_format(value) {
        let v = value.replace(/\s+/g, '').replace(/[^0-9]/gi, '')
        let matches = v.match(/\d{4,16}/g);
        let match = matches && matches[0] || ''
        let parts = []
    
        for (let i=0, len=match.length; i<len; i+=4) {
            parts.push(match.substring(i, i+4))
        }
    
        if (parts.length) {
            return parts.join(' ')
        } else {
            return value
        }
    }
    

    static ToggleSpinner (on_off)
    {
        const element = document.getElementById('cover-spin');
        if (on_off) element.style.display = "block";
        else element.style.display = "none";
    }


}

export default Helper;