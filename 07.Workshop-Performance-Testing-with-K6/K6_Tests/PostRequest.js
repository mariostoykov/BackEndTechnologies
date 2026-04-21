import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        {duration: '30s', target: 10}, // Ramp-up to 10 users over 30 seconds
        {duration: '1m', target: 10}, // Stay at 10 users for 1 minute
        { duration: '10s', target: 0}, // Ramp-down to 0 users over 10 seconds
    ],
};

export default function () {
    // Define the payload for the POST request
        const payload = JSON.stringify({
        name: 'Wayne Rooney',
        email: 'wayne.rooney@example.com',
        mobile: '+7894567890',
        comment: 'I have a question about my recent menu.',
        status: 'In Progress',
    });

    // Define the parameters, including headers
    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    // Send the POST request
    const res = http.post('http://localhost:5000/api/enquiry/', payload, params);

    // Check if the response status is 201 (Created)
    check(res, {
        'status is 200': (r) => r.status === 200,
        'contains expected fields': (r) => {
            const body = JSON.parse(r.body);
            return body.hasOwnProperty('_id') && body.hasOwnProperty('status');
        },
    })
    sleep(1);
}