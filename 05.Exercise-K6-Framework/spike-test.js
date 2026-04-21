import http from 'k6/http';

export const options = {
    stages: [
        {
            duration: "15s",
            target: 20
        },
        {
            duration: "1m",
            target: 20
        },
        {
            duration: "15s",
            target: 0
        },
    ]
};

export default function() {
    http.get('https://test.k6.io');
}