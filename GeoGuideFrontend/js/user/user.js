// js/user/user.js

import { createClient } from 'https://cdn.jsdelivr.net/npm/@supabase/supabase-js@2/+esm';

const SUPABASE_URL = 'https://zitxawcefyuhfhqnybyg.supabase.co';
const SUPABASE_ANON_KEY = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InppdHhhd2NlZnl1aGZocW55YnlnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTA3ODg3NzAsImV4cCI6MjA2NjM2NDc3MH0.UCZVX7p38R71IIyR8Nl_HY-w5USCl1wg-QhpCiwLgb8';

const supabase = createClient(SUPABASE_URL, SUPABASE_ANON_KEY);

const userInfoDisplay = document.getElementById('userInfoDisplay');
const authActions = document.getElementById('authActions'); 

async function updateUIBasedOnAuth(session) {
    if (session) {
        userInfoDisplay.innerHTML = `Your email: <strong>${session.user.email}</strong>`;
        userInfoDisplay.classList.remove('not-logged-in-message');
        userInfoDisplay.style.display = 'block';

        authActions.innerHTML = `
            <button class="btn btn-danger mt-3" id="logoutButton">
                <i class="bi bi-box-arrow-right"></i> Log Out
            </button>
        `;
        document.getElementById('logoutButton').addEventListener('click', async () => {
            const { error } = await supabase.auth.signOut();
            if (error) {
                console.error('Logout error:', error.message);
                alert('Logout failed: ' + error.message);
            } else {
                window.location.href = '../../user/login.html';
            }
        });
    } else {
        userInfoDisplay.innerHTML = `
            <div class="not-logged-in-message">
                You are not logged in. <br>
                <a href="../../user/login.html">Login here</a> or <a href="../../user/register.html">register an account</a> to manage your settings.
            </div>
        `;
        userInfoDisplay.style.display = 'block';

        authActions.innerHTML = `
            <button class="btn btn-primary mt-3 me-2" onclick="window.location.href='../../user/login.html'">
                <i class="bi bi-box-arrow-in-right"></i> Login
            </button>
            <button class="btn btn-success mt-3" onclick="window.location.href='../../user/register.html'">
                <i class="bi bi-person-plus"></i> Register
            </button>
        `;
    }
}

async function checkInitialSession() {
    const { data: { session }, error } = await supabase.auth.getSession();
    if (error) {
        console.error('Error fetching session:', error.message);
        userInfoDisplay.innerHTML = `<div class="alert alert-danger">Error loading user data: ${error.message}</div>`;
        userInfoDisplay.style.display = 'block';
    } else {
        updateUIBasedOnAuth(session);
    }
}

supabase.auth.onAuthStateChange((event, session) => {
    console.log('Auth state changed:', event, session);
    updateUIBasedOnAuth(session);
});

checkInitialSession();