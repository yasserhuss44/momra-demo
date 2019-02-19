import React, { Component } from 'react';
import joi from 'joi'
import Input from './input';

class Form extends Component {

    handleChange = ({currentTarget:input}) => {
        const errors = { ...this.state.errors };
        const errorMessage = this.validateProperty(input);
        if (errorMessage) errors[input.name] = errorMessage;
        else delete errors[input.name];
    
        const data={...this.state.data} ;
        data[input.name]=input.value;
        this.setState({errors,data});
    }
    handleSubmit = (e) => {
        e.preventDefault();

        const errors = this.validate();
        this.setState({ errors: errors ||{}});
        if (errors)
            return;
        this.doSubmit();
    }
    validateProperty = ({ name, value }) => {
        const obj = { [name]: value };
        const schema = { [name]: this.schema[name] };
        const { error } = joi.validate(obj, schema);
        return error ? error.details[0].message : null;
      };

    validate = () => {
        const option = { abortEarly: false };

        const validationResult = joi.validate(this.state.data, this.schema, option);
        if (!validationResult||!validationResult.error)
            return null;

        const errors = {};
        
        for (let item of validationResult.error.details)
        errors[item.path[0]] = item.message;
        this.setState({errors:errors});       
        return errors;
      

    }

    renderInput(name, label, type = 'text') {
        const {errors,data}=this.state;
        return <Input onChange={this.handleChange} value={data[name]} name={name} label={label} type={type} error={errors[name]} ></Input>

    }
    renderButton(label) {
        return <input type="submit" className="btn btn-primary" value={label} />
    }

}

export default Form;