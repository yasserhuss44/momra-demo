import React, { Component } from 'react';
import joi from 'joi'

import Form from './common/form'

class Register extends Form {
    state = {
        data: { name: '', username: '', password: '' },
        errors: {}
    }
    schema = {
        username: joi.string().required().email().label('Username'),
        password: joi.string().required().min(5).label('Password'),
        name: joi.string().required().min(6).label('name')
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                {this.renderInput('name', 'name')}
                {this.renderInput('username', 'Username')}
                {this.renderInput('password', 'Password', 'password')}
                {this.renderButton('Submit')}
            </form>
        );
    }
   
    doSubmit = () => {
        console.log('submitted',this.state.data);
    }
}

export default Register;