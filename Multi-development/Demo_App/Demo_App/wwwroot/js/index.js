
const navitemsEl = document.getElementById('demo');
function dropdownmenu123() {
    console.log("hi");
    navitemsEl.classList.toggle('collapse');
}



window.myBlazorInterop = {

    loadFileSharingTree: function () {
        $.ajax({
            url: "http://localhost:62869/api/FileManager/FileOperations",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Action: "read",
                Path: "/",
                ShowHiddenItems: true
            }),
            success: function (response) {

                //console.log(JSON.parse(response).cwd.name);

                response = JSON.parse(response)
                // Transform the API response to the desired format
                const transformedData = {
                    cwd: {
                        name: response.cwd.name,
                        hasChild: response.cwd.hasChild,
                        isFile: response.cwd.isFile
                    },
                    files: response.files.map(file => ({
                        name: file.name,
                        hasChild: file.hasChild,
                        isFile: file.isFile,
                        type: file.isFile ? file.type : undefined
                    }))
                };

                // Log the transformed data to ensure correctness
                console.log(transformedData);

                // Initialize jsTree with the transformed data
                $('#jstree').jstree({
                    core: {
                        data: [
                            {
                                text: transformedData.cwd.name,
                                state: { opened: true },
                                children: transformedData.files.map(file => ({
                                    text: file.name,
                                    icon: file.isFile ? "jstree-file" : "jstree-folder",
                                    children: file.hasChild
                                }))
                            }
                        ]
                    },
                    plugins: ["types"],
                    types: {
                        default: {
                            icon: "jstree-folder"
                        },
                        file: {
                            icon: "jstree-file"
                        }
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });

        // Add an event listener for the 'select_node' event
        $('#jstree').on('select_node.jstree', function (e, data) {
            // Pass the selected node ID to the C# method
            DotNet.invokeMethodAsync('Demo_App', 'OnNodeClick', data.node.id);
        });
    },

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
//$(document).read(function () {

//    // Call the download function when needed

//    $(".fileDwnBtn").click(function () {
//        console.log("clicked");
//        var filePath = $(this).attr("data-filepath");
//        var fileName = $(this).attr("data-file");
//        console.log("clicked");
//        console.log(filePath); console.log(fileName);
//        download(filePath, fileName);
//    });

//})