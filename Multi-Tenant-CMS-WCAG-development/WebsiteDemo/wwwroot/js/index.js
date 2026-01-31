



window.myBlazorInterop = {



    downloadFile: function (fileName, base64String) {
        console.log(base64String);
        console.log(fileName);


        try {

            // Create a link to trigger the download
            const a = document.createElement('a');
            a.href = base64String;
            a.download = fileName;
            document.body.appendChild(a);
            a.click();

            // Cleanup
            window.URL.revokeObjectURL(url);
            document.body.removeChild(a);
        } catch (error) {
            console.error('Error downloading file:', error);
        }
    }

}
