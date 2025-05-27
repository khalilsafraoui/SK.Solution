window.blazorCulture = {
    get: function () {
        const match = document.cookie.match(/\.AspNetCore\.Culture=([^;]+)/);
        if (!match) return null;

        // Decode the URL-encoded cookie value
        const decoded = decodeURIComponent(match[1]);

        const parts = decoded.split('|');
        for (let part of parts) {
            if (part.startsWith('c=')) {
                return part.substring(2); // remove "c="
            }
        }
        return null;
    }
};
